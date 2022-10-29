using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using Request.Response.Middleware.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request.Response.Middleware.Library.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestResponseOptions _requestResponseOptions;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next, RequestResponseOptions requestResponseOptions)
        {
            _next = next;
            _requestResponseOptions = requestResponseOptions;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            string requestBody = await GetRequestBody(context);

            Stream originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            Stopwatch stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            context.Request.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            RequestResponseContext requestResponseContext = new(context)
            {
                ResponseCreationTime = TimeSpan.FromTicks(stopwatch.ElapsedTicks),
                RequestBody = requestBody,
                ResponseBody= responseBodyText
            };

            _requestResponseOptions.RequestResponseHandler?.Invoke(requestResponseContext);
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream, Encoding.UTF8);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);

            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        private async Task<string> GetRequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            string requestBody = ReadStreamInChunks(requestStream);

            context.Request.Body.Seek(0,SeekOrigin.Begin);

            return requestBody;
        }
    }
}

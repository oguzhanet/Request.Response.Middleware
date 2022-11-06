using Request.Response.Middleware.FileLogger.Library.MessageCreators;
using Request.Response.Middleware.FileLogger.Library.Models;
using Request.Response.Middleware.Library;
using Request.Response.Middleware.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request.Response.Middleware.FileLogger.Library.LogWriters
{
    internal class FileLogWriter : ILogWriter
    {
        private readonly FileLoggingOptions _fileLoggerOptions;

        public FileLogWriter(FileLoggingOptions fileLoggerOptions)
        {
            _fileLoggerOptions = fileLoggerOptions;
            MessageCreator = fileLoggerOptions.UseJsonFormat
                ? new FileLoggerJsonMessageCreator()
                : new FileLoggerMessageCreator();

            fileLoggerOptions.ValidatePath();
        }

        public ILogMessageCreator MessageCreator { get; }

        public async Task Write(RequestResponseContext reqResContext)
        {
            var message = MessageCreator.Create(reqResContext);
            
            var fullPath = _fileLoggerOptions.GetFullFilePath();

            await File.AppendAllTextAsync(fullPath, message);
        }
    }
}

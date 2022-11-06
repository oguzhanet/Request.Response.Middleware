using Microsoft.AspNetCore.Builder;
using Request.Response.Middleware.FileLogger.Library.LogWriters;
using Request.Response.Middleware.FileLogger.Library.Models;
using Request.Response.Middleware.Library.Interfaces;
using Request.Response.Middleware.Library.Middlewares;
using System;

namespace Request.Response.Middleware.FileLogger.Library
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddRequestResponseFileLoggerMiddleware(this IApplicationBuilder applicationBuilder,
            Action<FileLoggingOptions> optionAction)
        {
            var reqResOption = new FileLoggingOptions();
            optionAction(reqResOption);

            ILogWriter logWriter = new FileLogWriter(reqResOption);

            applicationBuilder.UseMiddleware<RequestResponseLoggingMiddleware>(logWriter);

            return applicationBuilder;
        }
    }
}

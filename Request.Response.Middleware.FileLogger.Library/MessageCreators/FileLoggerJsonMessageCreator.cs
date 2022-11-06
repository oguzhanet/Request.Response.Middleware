using Request.Response.Middleware.Library;
using Request.Response.Middleware.Library.Interfaces;
using System;
using System.Text.Json;

namespace Request.Response.Middleware.FileLogger.Library.MessageCreators
{
    internal class FileLoggerJsonMessageCreator : ILogMessageCreator
    {
        public string Create(RequestResponseContext reqResContext)
        {
            return $"{DateTime.Now:dd.MM.yyyy HH:mm.ss} - {JsonSerializer.Serialize(reqResContext)}\n";
        }
    }
}

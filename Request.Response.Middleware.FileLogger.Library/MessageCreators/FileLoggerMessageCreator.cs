using Request.Response.Middleware.Library;
using Request.Response.Middleware.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request.Response.Middleware.FileLogger.Library.MessageCreators
{
    internal class FileLoggerMessageCreator : ILogMessageCreator
    {
        public string Create(RequestResponseContext reqResContext)
        {
            string message = $"{DateTime.Now:dd.MM.yyyy HH:mm.ss} - [{reqResContext.FormattedCreationTime}] [{reqResContext.Url.PathAndQuery}] " +
                $"[{reqResContext.RequestBody}] [{reqResContext.ResponseBody}]\n";

            return message;
        }
    }
}

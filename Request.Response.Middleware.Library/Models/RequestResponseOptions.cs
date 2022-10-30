namespace Request.Response.Middleware.Library.Models
{
    public class RequestResponseOptions
    {
        internal Func<RequestResponseContext,Task> RequestResponseHandler { get; set; }
        internal ILoggerFactory LoggerFactory;
        internal LoggingOptions LoggingOptions;

        public void UseHandler(Func<RequestResponseContext, Task> requestResponseHandler)
        {
            RequestResponseHandler = requestResponseHandler;
        }

        public void UseLogger(ILoggerFactory loggerFavtory, Action<LoggingOptions> loggingAction)
        {
            LoggingOptions = new LoggingOptions();
            loggingAction(LoggingOptions);

            LoggerFactory = loggerFavtory;
        }
    }
}

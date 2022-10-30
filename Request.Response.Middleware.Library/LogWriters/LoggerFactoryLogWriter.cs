namespace Request.Response.Middleware.Library.LogWriters
{
    internal class LoggerFactoryLogWriter : ILogWriter
    {
        private readonly ILogger _logger;
        private readonly LoggingOptions _loggingOptions;

        public ILogMessageCreator MessageCreator { get; }

        internal LoggerFactoryLogWriter(ILoggerFactory loggerFactory, LoggingOptions loggingOptions)
        {
            _logger = loggerFactory.CreateLogger(loggingOptions.LoggerCategoryName);
            _loggingOptions = loggingOptions;

            MessageCreator = new LoggerFactoryMessageCreator(loggingOptions);
        }


        public async Task Write(RequestResponseContext reqResContext)
        {
            var message = MessageCreator.Create(reqResContext);
            _logger.Log(_loggingOptions.LogLevel, message);

            await Task.CompletedTask;
        }
    }
}

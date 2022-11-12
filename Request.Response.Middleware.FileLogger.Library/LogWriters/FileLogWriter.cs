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

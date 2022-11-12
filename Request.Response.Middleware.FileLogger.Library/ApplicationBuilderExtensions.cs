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

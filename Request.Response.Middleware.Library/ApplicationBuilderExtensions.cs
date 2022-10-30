namespace Request.Response.Middleware.Library
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddRequestResponseMiddleware(this IApplicationBuilder applicationBuilder,
            Action<RequestResponseOptions> optionAction)
        {
            var reqResOption = new RequestResponseOptions();
            optionAction(reqResOption);

            ILogWriter logWriter = reqResOption.LoggerFactory is null
                ? new NullLogWriter()
                : new LoggerFactoryLogWriter(reqResOption.LoggerFactory, reqResOption.LoggingOptions);

            if (reqResOption.RequestResponseHandler is not null)
                applicationBuilder.UseMiddleware<HandlerRequestResponseLoggingMiddleware>(reqResOption.RequestResponseHandler, logWriter);
            else
                applicationBuilder.UseMiddleware<RequestResponseLoggingMiddleware>(reqResOption.RequestResponseHandler, logWriter);

            return applicationBuilder;
        }
    }
}

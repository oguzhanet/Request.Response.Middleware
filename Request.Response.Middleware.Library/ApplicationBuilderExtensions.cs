namespace Request.Response.Middleware.Library
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddRequestResponseMiddleware(this IApplicationBuilder applicationBuilder,
            Action<RequestResponseOptions> optionAction)
        {
            var reqResOption = new RequestResponseOptions();
            optionAction(reqResOption);

            if (reqResOption.RequestResponseHandler is null && reqResOption.LoggerFactory is null)
                throw new ArgumentNullException($"{nameof(reqResOption.RequestResponseHandler)} and {nameof(reqResOption.LoggerFactory)}");

            ILogWriter logWriter = reqResOption.LoggerFactory is null
                ? new NullLogWriter()
                : new LoggerFactoryLogWriter(reqResOption.LoggerFactory, reqResOption.LoggingOptions);

            if (reqResOption.RequestResponseHandler is not null)
                applicationBuilder.UseMiddleware<HandlerRequestResponseLoggingMiddleware>(reqResOption.RequestResponseHandler, logWriter);
            else
                applicationBuilder.UseMiddleware<RequestResponseLoggingMiddleware>(logWriter);

            return applicationBuilder;
        }
    }
}

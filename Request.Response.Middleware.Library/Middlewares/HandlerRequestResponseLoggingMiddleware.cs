namespace Request.Response.Middleware.Library.Middlewares
{
    public class HandlerRequestResponseLoggingMiddleware: BaseRequestResponseMiddleware
    {
        private readonly Func<RequestResponseContext, Task> _requestResponseHandler;

        public HandlerRequestResponseLoggingMiddleware(RequestDelegate next, Func<RequestResponseContext,Task> requestResponseHandler, ILogWriter logWriter) :base(next,logWriter)
        {
            _requestResponseHandler = requestResponseHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestResponseContext = await BaseMiddlewareInvoke(context);

            await _requestResponseHandler.Invoke(requestResponseContext);

            //_requestResponseOptions.RequestResponseHandler.Invoke(requestResponseContext);
        }
    }
}

namespace Request.Response.Middleware.Library.Middlewares
{
    public class RequestResponseLoggingMiddleware: BaseRequestResponseMiddleware
    {
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogWriter logWriter) : base(next, logWriter)
        {
        }

        public async Task Invoke(HttpContext context)
        {
            await BaseMiddlewareInvoke(context);
        }
    }
}

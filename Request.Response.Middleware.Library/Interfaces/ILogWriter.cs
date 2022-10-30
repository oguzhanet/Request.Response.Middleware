namespace Request.Response.Middleware.Library.Interfaces
{
    public interface ILogWriter
    {
        ILogMessageCreator MessageCreator { get; }

        Task Write(RequestResponseContext reqResContext);
    }
}

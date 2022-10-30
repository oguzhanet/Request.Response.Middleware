namespace Request.Response.Middleware.Library.LogWriters
{
    internal class NullLogWriter : ILogWriter
    {
        public ILogMessageCreator MessageCreator => throw new NotImplementedException();

        public Task Write(RequestResponseContext reqResContext)
        {
            return Task.CompletedTask;
        }
    }
}

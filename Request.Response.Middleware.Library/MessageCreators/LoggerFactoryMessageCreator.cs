namespace Request.Response.Middleware.Library.MessageCreators
{
    internal class LoggerFactoryMessageCreator : BaseLogMessageCreator, ILogMessageCreator
    {
        private RequestResponseContext _reqResContext;
        private readonly LoggingOptions _loggingOptions;

        public LoggerFactoryMessageCreator(LoggingOptions loggingOptions)
        {
            _loggingOptions = loggingOptions;

        }

        public string Create(RequestResponseContext reqResContext)
        {
            _reqResContext = reqResContext;
            var stringBuilder = new StringBuilder();

            foreach (var field in _loggingOptions.LoggingFields)
            {
                var value = GetValueByField(reqResContext, field);

                stringBuilder.AppendFormat("{0}: {1}\n", field, value);
            }

            return stringBuilder.ToString();
        }

        
    }
}

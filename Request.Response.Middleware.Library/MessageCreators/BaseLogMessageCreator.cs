namespace Request.Response.Middleware.Library.MessageCreators
{
    public abstract class BaseLogMessageCreator
    {
        protected string GetValueByField(RequestResponseContext reqResContext, LogFields logField)
        {
            return logField switch
            {
                LogFields.Request => reqResContext.RequestBody,
                LogFields.Response => reqResContext.ResponseBody,
                LogFields.QueryString => reqResContext._context?.Request?.QueryString.Value,
                LogFields.Path => reqResContext._context?.Request?.Path.Value,
                LogFields.HostName => reqResContext._context?.Request?.Host.Value,
                LogFields.RequestLength => reqResContext.RequestLength.ToString(),
                LogFields.ResponseLength => reqResContext.ResponseLength.ToString(),
                LogFields.ResponseTiming => reqResContext.FormattedCreationTime,
                _ => string.Empty
            };
        }
    }
}

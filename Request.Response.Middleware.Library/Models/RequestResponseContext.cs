namespace Request.Response.Middleware.Library.Models
{
    public class RequestResponseContext
    {
        internal readonly HttpContext _context;

        public RequestResponseContext(HttpContext context)
        {
            _context = context;
        }

        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }

        [JsonIgnore]
        public TimeSpan ResponseCreationTime { get; set; }
        public string FormattedCreationTime =>
            FormattedCreationTime is null
                ? "00.00.000"
                : string.Format("{0:mm\\:ss\\.fff}", ResponseCreationTime);

        public Uri Url => BuildUrl();

        public int? RequestLength => RequestBody?.Length;
        public int? ResponseLength => ResponseBody?.Length;

        internal Uri BuildUrl()
        {
            string url = _context.Request.GetDisplayUrl();

            return new Uri(url, UriKind.RelativeOrAbsolute);
        }
    }
}

using Newtonsoft.Json;

namespace PersonDirectory.Api.Extensions
{
    public static partial class ExceptionMiddlewareExtensions
    {
        public class ErrorDetails
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }
    }
}
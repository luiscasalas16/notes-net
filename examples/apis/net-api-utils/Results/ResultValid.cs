using System.Net;

namespace net_api_utils.Results
{
    public class ResultValid : Result
    {
        private const HttpStatusCode statusCode = HttpStatusCode.OK;

        public ResultValid(object content)
            : base(content, statusCode) { }
    }
}

using System.Net;
using System.Net.Http;

namespace NetFwApi.Common.Results
{
    public class ResultValid : Result
    {
        private const HttpStatusCode statusCode = HttpStatusCode.OK;

        public ResultValid(HttpRequestMessage request, object content)
            : base(content, statusCode, request) { }
    }
}

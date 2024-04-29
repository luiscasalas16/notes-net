using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace netfw_api_utils.Results
{
    public class ResultError : Result
    {
        private const HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        public ResultError(HttpRequestMessage request, string error)
            : base(new List<ResultMessage> { new ResultMessage(error) }, statusCode, request) { }

        public ResultError(HttpRequestMessage request, params string[] error)
            : base(
                new List<ResultMessage>(error.Select(t => new ResultMessage(t))),
                statusCode,
                request
            ) { }
    }
}

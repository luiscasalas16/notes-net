using System.Net;

namespace net_api_utils.Results
{
    public class ResultError : Result
    {
        private const HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        public ResultError(string error)
            : base(new List<ResultMessage> { new ResultMessage(error) }, statusCode) { }

        public ResultError(params string[] error)
            : base(new List<ResultMessage>(error.Select(t => new ResultMessage(t))), statusCode) { }
    }
}

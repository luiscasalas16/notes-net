using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace netfw_api_utils.Results
{
    public class Result : IHttpActionResult
    {
        private readonly object _content;
        private readonly HttpStatusCode _statusCode;
        private readonly HttpRequestMessage _request;

        public Result(object content, HttpStatusCode statusCode, HttpRequestMessage request)
        {
            _content = content;
            _statusCode = statusCode;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (_content == null)
                return Task.FromResult(_request.CreateResponse(_statusCode));
            else
                return Task.FromResult(_request.CreateResponse(_statusCode, _content));
        }
    }
}

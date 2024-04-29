using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace net_api_utils.Results
{
    public abstract class Result : IActionResult
    {
        private readonly object _content;
        private readonly HttpStatusCode _statusCode;

        public Result(object content, HttpStatusCode statusCode)
        {
            _content = content;
            _statusCode = statusCode;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            if (_content == null)
            {
                var result = new StatusCodeResult((int)_statusCode);

                return result.ExecuteResultAsync(context);
            }
            else
            {
                var result = new ObjectResult(_content) { StatusCode = (int)_statusCode };

                return result.ExecuteResultAsync(context);
            }
        }
    }
}

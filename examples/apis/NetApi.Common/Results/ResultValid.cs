using System.Net;

namespace NetApi.Common.Results
{
    public class ResultValid : Result
    {
        private const HttpStatusCode statusCode = HttpStatusCode.OK;

        public ResultValid(object content)
            : base(content, statusCode) { }
    }
}

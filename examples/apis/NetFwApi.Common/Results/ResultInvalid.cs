using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.ModelBinding;

namespace NetFwApi.Common.Results
{
    public class ResultInvalid : Result
    {
        private const HttpStatusCode statusCode = HttpStatusCode.BadRequest;

        public ResultInvalid(HttpRequestMessage request, string error)
            : base(new List<ResultMessage> { new ResultMessage(error) }, statusCode, request) { }

        public ResultInvalid(HttpRequestMessage request, List<string> errors)
            : base(
                new List<ResultMessage>(errors.Select(t => new ResultMessage(t)).ToList()),
                statusCode,
                request
            ) { }

        public ResultInvalid(HttpRequestMessage request, ModelStateDictionary modelState)
            : base(
                modelState
                    .Values.SelectMany(m => m.Errors)
                    .Select(e => new ResultMessage(e.ErrorMessage))
                    .ToList(),
                statusCode,
                request
            ) { }
    }
}

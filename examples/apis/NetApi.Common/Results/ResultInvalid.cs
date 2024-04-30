using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NetApi.Common.Results
{
    public class ResultInvalid : Result
    {
        private const HttpStatusCode statusCode = HttpStatusCode.BadRequest;

        public ResultInvalid(string error)
            : base(new List<ResultMessage> { new ResultMessage(error) }, statusCode) { }

        public ResultInvalid(List<string> errors)
            : base(
                new List<ResultMessage>(errors.Select(t => new ResultMessage(t)).ToList()),
                statusCode
            ) { }

        public ResultInvalid(ModelStateDictionary modelState)
            : base(
                modelState
                    .Values.SelectMany(m => m.Errors)
                    .Select(e => new ResultMessage(e.ErrorMessage))
                    .ToList(),
                statusCode
            ) { }
    }
}

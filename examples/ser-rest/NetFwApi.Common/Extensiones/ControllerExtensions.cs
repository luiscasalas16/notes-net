using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using NetFwApi.Common.Results;

namespace NetFwApi.Common.Extensions
{
    public static class ControllerExtensions
    {
        public static Result ResultSuccess(this ApiController controller)
        {
            return Result.Success(controller.Request);
        }

        public static Result ResultSuccess<TValue>(this ApiController controller, TValue data)
        {
            return Result.Success(data, controller.Request);
        }

        public static Result ResultFailure(this ApiController controller, string error)
        {
            return Result.Failure(new ResultError("", error), controller.Request);
        }

        public static Result ResultFailure(this ApiController controller, ModelStateDictionary modelState)
        {
            return Result.Failure(modelState.Values.SelectMany(m => m.Errors).Select(e => new ResultError("", e.ErrorMessage)).ToList(), controller.Request);
        }
    }
}

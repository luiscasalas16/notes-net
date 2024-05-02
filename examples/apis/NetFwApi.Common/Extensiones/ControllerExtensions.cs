using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using NetFwApi.Common.Results;

namespace NetFwApi.Common.Extensions
{
    public static class ControllerExtensions
    {
        public static Result ResultValid(this ApiController controller)
        {
            return Result.Success(controller.Request);
        }

        public static Result ResultValid<TValue>(this ApiController controller, TValue data)
        {
            return Result.Success(data, controller.Request);
        }

        public static Result ResultInvalid(this ApiController controller, string error)
        {
            return Result.Failure(new Error("", error), controller.Request);
        }

        public static Result ResultInvalid(this ApiController controller, ModelStateDictionary modelState)
        {
            return Result.Failure(modelState.Values.SelectMany(m => m.Errors).Select(e => new Error("", e.ErrorMessage)).ToList(), controller.Request);
        }

        public static bool Validate<T>(this ApiController controller, T model, out Result resultinvalid)
        {
            if (model == null)
            {
                model = (T)Activator.CreateInstance(typeof(T), new object[] { });

                controller.Validate(model);
            }

            if (!controller.ModelState.IsValid)
                resultinvalid = controller.ResultInvalid(controller.ModelState);
            else
                resultinvalid = null;

            return resultinvalid != null;
        }
    }
}

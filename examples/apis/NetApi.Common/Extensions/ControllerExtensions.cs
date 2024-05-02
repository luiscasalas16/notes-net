using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetApi.Common.Results;

namespace NetApi.Common.Extensions
{
    public static class ControllerExtensions
    {
        public static Result ResultValid(this ControllerBase controller)
        {
            return Result.Success();
        }

        public static Result ResultValid(this ControllerBase controller, object data)
        {
            return Result.Success(data);
        }

        public static Result ResultInvalid(this ControllerBase controller, string error)
        {
            return Result.Failure(new Error("", error));
        }

        public static Result ResultInvalid(this ControllerBase controller, ModelStateDictionary modelState)
        {
            return Result.Failure(modelState.Values.SelectMany(m => m.Errors).Select(e => new Error("", e.ErrorMessage)).ToList());
        }

        public static bool Validate<T>(this ControllerBase controller, T model, out Result resultinvalid)
        {
            if (model == null)
            {
                model = (T)Activator.CreateInstance(typeof(T), new object[] { })!;

                controller.TryValidateModel(model);
            }

            if (!controller.ModelState.IsValid)
                resultinvalid = controller.ResultInvalid(controller.ModelState);
            else
                resultinvalid = null!;

            return resultinvalid != null;
        }
    }
}

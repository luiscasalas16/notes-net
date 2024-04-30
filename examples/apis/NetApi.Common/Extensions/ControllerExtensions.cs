using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetApi.Common.Results;

namespace NetApi.Common.Extensions
{
    public static class ControllerExtensions
    {
        public static Result ResultValid(this ControllerBase controller)
        {
            return new ResultValid(null!);
        }

        public static Result ResultValid(this ControllerBase controller, object data)
        {
            return new ResultValid(data);
        }

        public static Result ResultInvalid(this ControllerBase controller, string error)
        {
            return new ResultInvalid(error);
        }

        public static Result ResultInvalid(
            this ControllerBase controller,
            ModelStateDictionary modelState
        )
        {
            return new ResultInvalid(
                modelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            );
        }

        public static Result ResultUnexpected(this ControllerBase controller, string error)
        {
            return new ResultError(error);
        }

        public static bool Validate<T>(
            this ControllerBase controller,
            T model,
            out Result resultinvalid
        )
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

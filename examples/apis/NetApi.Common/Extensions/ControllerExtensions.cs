using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Results;

namespace NetApi.Common.Extensions
{
    public static class ControllerExtensions
    {
        public static bool Validate<T>(this ControllerBase controller, T model, out Result resultinvalid)
        {
            if (model == null)
            {
                model = (T)Activator.CreateInstance(typeof(T), new object[] { })!;

                controller.TryValidateModel(model);
            }

            if (!controller.ModelState.IsValid)
                resultinvalid = Result.Failure(controller.ModelState.Values.SelectMany(m => m.Errors).Select(e => new ResultError("", e.ErrorMessage)).ToList());
            else
                resultinvalid = null!;

            return resultinvalid != null;
        }
    }
}

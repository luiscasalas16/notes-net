using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using NetFwApi.Common.Results;

namespace NetFwApi.Common.Extensions
{
    public static class ControllerExtensions
    {
        public static Result ResultValid(this ApiController controller, object data = null)
        {
            return new ResultValid(controller.Request, data);
        }

        public static Result ResultInvalid(this ApiController controller, string error)
        {
            return new ResultInvalid(controller.Request, error);
        }

        public static Result ResultInvalid(
            this ApiController controller,
            ModelStateDictionary modelState
        )
        {
            return new ResultInvalid(
                controller.Request,
                modelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            );
        }

        public static Result ResultUnexpected(this ApiController controller, string error)
        {
            return new ResultError(controller.Request, error);
        }

        public static bool Validate<T>(
            this ApiController controller,
            T model,
            out Result resultinvalid
        )
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

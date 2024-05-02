using System.Linq;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NetFwApi.Common.Results;

namespace NetFwApi.Filters
{
    // Enables automatic validation of objects that have validation annotations.

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var result = Result.Failure(actionContext.ModelState.Values.SelectMany(m => m.Errors).Select(e => new Error("", e.ErrorMessage)).ToList(), actionContext.Request);

                actionContext.Response = result.ExecuteAsync(CancellationToken.None).Result;
            }
        }
    }
}

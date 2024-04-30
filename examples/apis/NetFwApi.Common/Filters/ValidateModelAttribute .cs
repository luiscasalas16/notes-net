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
            if (actionContext.ModelState.IsValid == false)
                actionContext.Response = new ResultInvalid(
                    actionContext.Request,
                    actionContext.ModelState
                )
                    .ExecuteAsync(CancellationToken.None)
                    .Result;
        }
    }
}

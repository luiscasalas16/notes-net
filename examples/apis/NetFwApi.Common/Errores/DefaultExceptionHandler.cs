using System.Collections.Generic;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using NetFwApi.Common.Results;

namespace NetFwApi.Common.Errores
{
    // Implements default exception handler.

    public class DefaultExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is ValidationException errorValidacion)
            {
                var result = new ResultClientError(new List<ResultError>() { new ResultError("", errorValidacion.Message) });

                context.Result = new Result<ResultClientError>(result.Status, result, context.Request);
            }
            else if (context.Exception is ConfigurationException errorConfiguracion)
            {
                var result = new ResultServerError(errorConfiguracion.Message);

                context.Result = new Result<ResultServerError>(result.Status, result, context.Request);
            }
            else
            {
                var result = new ResultServerError(context.Exception);

                context.Result = new Result<ResultServerError>(result.Status, result, context.Request);
            }
        }
    }
}

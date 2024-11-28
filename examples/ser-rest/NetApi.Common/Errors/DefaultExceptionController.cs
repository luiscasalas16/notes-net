using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetApi.Common.Results;

namespace NetApi.Common.Errores
{
    // Implements default exception handler.

    [ApiController]
    public class DefaultExceptionController : ControllerBase
    {
        [Route("/error")]
        public IActionResult ErrorLocalDevelopment([FromServices] ILogger<DefaultExceptionController> logger)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            logger.LogError(context.Error, "internal api error");

            if (context.Error is ValidationException errorValidacion)
            {
                var result = new ResultClientError([new ResultError("", errorValidacion.Message)]);

                return new JsonResult(result) { StatusCode = result.Status };
            }
            else if (context.Error is ConfigurationException errorConfiguracion)
            {
                var result = new ResultServerError(errorConfiguracion.Message);

                return new JsonResult(result) { StatusCode = result.Status };
            }
            else
            {
                var result = new ResultServerError(context.Error);

                return new JsonResult(result) { StatusCode = result.Status };
            }
        }
    }
}

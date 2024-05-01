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
                return new ResultInvalid(errorValidacion.Message);
            }
            else if (context.Error is ConfigurationException errorConfiguracion)
            {
                return new ResultError(errorConfiguracion.Message);
            }
            else
            {
                return new ResultError("internal api error", context.Error.Message);
            }
        }
    }
}

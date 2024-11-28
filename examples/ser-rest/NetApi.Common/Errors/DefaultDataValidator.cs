using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetApi.Common.Errors
{
    public static class DefaultDataValidator
    {
        public static (List<ValidationResult> Results, bool IsValid) DataAnnotationsValidate(this object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);

            var isValid = Validator.TryValidateObject(model, context, results, true);

            return (results, isValid);
        }

        public static RouteHandlerBuilder Validate<T>(this RouteHandlerBuilder builder)
        {
            builder.AddEndpointFilter(
                async (invocationContext, next) =>
                {
                    var argument = invocationContext.Arguments.OfType<T>().FirstOrDefault();

                    ArgumentNullException.ThrowIfNull(argument);

                    var response = argument.DataAnnotationsValidate();

                    if (!response.IsValid)
                        throw new Errores.ValidationException(response.Results);

                    return await next(invocationContext);
                }
            );

            return builder;
        }
    }
}

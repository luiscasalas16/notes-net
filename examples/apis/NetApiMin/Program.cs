using NetApi.Common.Errors;
using NetApiMin.Endpoints;

namespace NetApiMin
{
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Implements default exception handler.
            builder.Services.Configure<RouteHandlerOptions>(o =>
            {
                o.ThrowOnBadRequest = true;
            });
            builder.Services.AddExceptionHandler<DefaultExceptionMinimal>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            // Implements default exception handler.
            app.UseExceptionHandler();

            Test1.MapEndpoints(app);
            Test2.MapEndpoints(app);
            Test3.MapEndpoints(app);
            Test4.MapEndpoints(app);
            Test5.MapEndpoints(app);

            app.Run();
        }
    }
}

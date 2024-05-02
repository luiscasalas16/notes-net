using NetApiMin.Endpoints;

namespace NetApiMin
{
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();

            Test1.MapEndpoints(app);
            Test2.MapEndpoints(app);
            Test3.MapEndpoints(app);
            Test4.MapEndpoints(app);
            Test5.MapEndpoints(app);

            app.Run();
        }
    }
}

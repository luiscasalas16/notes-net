namespace NetBaseWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => new { message = "Hello World!" });

            app.Run();
        }
    }
}

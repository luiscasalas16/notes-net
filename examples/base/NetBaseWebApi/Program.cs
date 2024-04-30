using NetBaseWebApi.Endpoints;

namespace NetBaseWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Enable Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Enable Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            Home.Map(app);

            app.Run();
        }
    }
}

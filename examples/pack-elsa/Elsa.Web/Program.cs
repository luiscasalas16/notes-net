using Elsa.Extensions;
using ElsaWeb.Workflows;

var builder = WebApplication.CreateBuilder(args);

// Add services.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddElsa(elsa =>
{
    elsa.AddWorkflow<HttpHelloWorld>();
    elsa.UseHttp(http =>
        http.ConfigureHttpOptions = options =>
        {
            options.BaseUrl = new Uri("https://localhost:5001");
            options.BasePath = "/workflows";
        }
    );
});

var app = builder.Build();

// Configure services.

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseWorkflows();

await app.RunAsync();

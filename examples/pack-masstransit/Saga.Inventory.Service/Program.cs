using Inventory.Service.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ReserveInventoryConsumer>();

    x.UsingAzureServiceBus(
        (context, configurator) =>
        {
            configurator.Host(builder.Configuration.GetValue<string>("ServiceBusKey"));

            configurator.ConfigureEndpoints(context);
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

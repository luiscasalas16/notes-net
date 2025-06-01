using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetBaseConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using IHost host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices(
                        (hostContext, services) =>
                        {
                            services.AddHostedService<BasicPublisher>();

                            services.AddMassTransit(x =>
                            {
                                x.SetKebabCaseEndpointNameFormatter();

                                x.AddConsumer<BasicConsumer>();

                                x.UsingAzureServiceBus(
                                    (context, configurator) =>
                                    {
                                        configurator.Host(
                                            hostContext.Configuration.GetValue<string>(
                                                "ServiceBusKey"
                                            )
                                        );

                                        configurator.ConfigureEndpoints(context);
                                    }
                                );
                            });
                        }
                    )
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Global.Helpers.LogFailure(ex);
            }

            Global.Helpers.ConsoleWait();
        }
    }
}

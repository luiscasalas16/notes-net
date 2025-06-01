using MassTransit;
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
                        (services) =>
                        {
                            services.AddHostedService<BasicPublisher>();

                            services.AddMassTransit(x =>
                            {
                                x.SetKebabCaseEndpointNameFormatter();

                                x.AddConsumer<BasicConsumer>();

                                x.UsingInMemory(
                                    (context, cfg) =>
                                    {
                                        cfg.ConfigureEndpoints(context);
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

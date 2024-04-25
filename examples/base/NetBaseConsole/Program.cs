using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetBaseConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using IHost host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((services) =>
                    {
                        services.AddHostedService<Application>();
                    })
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Global.Helpers.LogFailure(ex);
            }

            Global.Helpers.ConsoleWait();
        }

        public class Application : BackgroundService
        {
            private readonly ILogger<Application> _logger;
            private readonly IConfiguration _configuration;

            public Application(ILogger<Application> logger, IConfiguration configuration)
            {
                _logger = logger;
                _configuration = configuration;
            }

            protected override Task ExecuteAsync(CancellationToken stoppingToken)
            {
                Global.Helpers.LogSuccess(".Net Console");

                return Task.CompletedTask;
            }
        }
    }
}
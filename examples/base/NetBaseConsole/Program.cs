using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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
                Trace.WriteLine(ex.ToString());

                Console.WriteLine(ex.ToString());
            }
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

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                Console.WriteLine(".Net Console");
                Console.WriteLine("Hello World " + DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd_HH-mm-ss-fffff"));
            }
        }
    }
}
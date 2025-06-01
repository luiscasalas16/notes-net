using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetBaseConsole
{
    class BasicPublisher : BackgroundService
    {
        private readonly ILogger<BasicPublisher> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBus _bus;

        public BasicPublisher(
            ILogger<BasicPublisher> logger,
            IConfiguration configuration,
            IBus bus
        )
        {
            _logger = logger;
            _bus = bus;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = new BasicMessage { Value = $"The time is {DateTimeOffset.Now}" };

                await _bus.Publish(message, stoppingToken);

                _logger.LogInformation("Publish message: {Text}", message.Value);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}

using MassTransit;
using Microsoft.Extensions.Logging;

namespace NetBaseConsole
{
    public class BasicConsumer : IConsumer<BasicMessage>
    {
        readonly ILogger<BasicConsumer> _logger;

        public BasicConsumer(ILogger<BasicConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<BasicMessage> context)
        {
            _logger.LogInformation("Received message: {Text}", context.Message.Value);

            return Task.CompletedTask;
        }
    }
}

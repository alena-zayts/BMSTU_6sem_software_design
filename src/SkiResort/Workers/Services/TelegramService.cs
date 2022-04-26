using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace Workers
{
    public class TelegramService : BackgroundService
    {
        private readonly ILogger<TelegramService> _logger;

        public TelegramService(ILogger<TelegramService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TelegramBotClient Bot = new(Configuration.BotToken);

            ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
            Bot.StartReceiving(Handlers.HandleUpdateAsync,
                               Handlers.HandleErrorAsync,
                               receiverOptions,
                               stoppingToken);

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Telegram.Bot.Examples.Polling
{

    public static class Program
    {
        private static TelegramBotClient? Bot;

        public static async Task Main()
        {
            Bot = new TelegramBotClient(Configuration.BotToken);

            using var cts = new CancellationTokenSource();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
            Bot.StartReceiving(Handlers.HandleUpdateAsync,
                               Handlers.HandleErrorAsync,
                               receiverOptions,
                               cts.Token);

            Console.ReadLine();

            // Send cancellation request to stop bot
            cts.Cancel();
        }
    }
}

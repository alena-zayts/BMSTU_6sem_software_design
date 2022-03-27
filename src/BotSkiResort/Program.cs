using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot
{
    class Program
    {
        private static string Token { get; set; } = "5116348352:AAGc2737xTkAOiKg8oXuQeLAbKq5e0IdIVg";
        private static TelegramBotClient client;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(Token);
            //client.StartReceiving();
            //client.OnMessage += OnMessageHandler;
            //Console.ReadLine();
            //client.StopReceiving();
        }

        //private static async void OnMessageHandler(object sender, MessageEventArgs e)
        //{
        //    var msg = e.Message;
        //    if (msg.Text != null)
        //    {
        //        Console.WriteLine($"Пришло сообщение с текстом: {msg.Text}");
        //        switch (msg.Text)
        //        {
        //            case "Стикер":
        //                await client.SendStickerAsync(
        //                    chatId: msg.Chat.Id,
        //                    sticker: "Ссылка на стикер",
        //                    replyToMessageId: msg.MessageId,
        //                    replyMarkup: GetButtons());
        //                break;
        //            case "Картинка":
        //                await client.SendPhotoAsync(
        //                    chatId: msg.Chat.Id,
        //                    photo: "Ссылка на картинку",
        //                    replyMarkup: GetButtons());
        //                break;

        //            default:
        //                await client.SendTextMessageAsync(msg.Chat.Id, "Выберите команду: ", replyMarkup: GetButtons());
        //                break;
        //        }
        //    }
        //}

        //private static IReplyMarkup GetButtons()
        //{
        //    return new ReplyKeyboardMarkup
        //    {
        //        Keyboard = new List<List<KeyboardButton>>
        //        {
        //            new List<KeyboardButton>{ new KeyboardButton { Text = "Стикер"}, new KeyboardButton { Text = "Картинка"} },
        //            new List<KeyboardButton>{ new KeyboardButton { Text = "123"}, new KeyboardButton { Text = "456"} }
        //        }
        //    };
        //}
    }
}
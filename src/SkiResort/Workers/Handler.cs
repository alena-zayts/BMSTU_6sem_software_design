using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using BL.Models;
namespace Workers
{
    public class Handler
    {
        private PermissionsEnum _permissions = PermissionsEnum.UNAUTHORIZED;
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }








        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                // UpdateType.EditedMessage => BotOnMessageReceived(botClient, update.EditedMessage!),
                // UpdateType.InlineQuery => BotOnInlineQueryReceived(botClient, update.InlineQuery!),
                // UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(botClient, update.ChosenInlineResult!),

                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(botClient, update.CallbackQuery!),
                _ => UnknownUpdateHandlerAsync(botClient, update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }






        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Telegram.Bot.Types.Message message)
        {
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("email")) //или текст, который вы отправляли
            {
                return;
                // тут обрабатываете уже message.Text и дальше отправляете пользователю, например, ссылку или что-то еще..
            }

            var action = message.Text!.Split(' ')[0] switch
            {
                "/inline" => SendInlineKeyboard(botClient, message),
                //"/keyboard" => SendReplyKeyboard(botClient, message),
                //"/remove" => RemoveKeyboard(botClient, message),
                _ => Usage(botClient, message)
            };
            Telegram.Bot.Types.Message sentMessage = await action;
            Console.WriteLine($"The message was sent with id: {sentMessage.MessageId}");



            static async Task<Telegram.Bot.Types.Message> SendInlineKeyboard(ITelegramBotClient botClient, Telegram.Bot.Types.Message message)
            {
                await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                // Simulate longer running task
                await Task.Delay(500);

                InlineKeyboardMarkup inlineKeyboard = new(
                    new[]
                    {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Регистрация", "Регистрация"),
                        InlineKeyboardButton.WithCallbackData("Авторизация", "Авторизация"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Информация о подъемнике", "Информация о подъемнике"),
                        InlineKeyboardButton.WithCallbackData("Информация о спуске", "Информация о спуске"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Куда ведет подъемник", "Куда ведет подъемник"),
                        InlineKeyboardButton.WithCallbackData("Как добраться до трассы", "Как добраться до трассы"),
                    },
                    });

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Choose",
                                                            replyMarkup: inlineKeyboard);
            }






            static async Task<Telegram.Bot.Types.Message> Usage(ITelegramBotClient botClient, Telegram.Bot.Types.Message message)
            {
                const string usage = "Usage:\n" +
                                     "/inline   - send inline keyboard\n" +
                                     "/keyboard - send custom keyboard\n" +
                                     "/remove   - remove custom keyboard\n";

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: usage,
                                                            replyMarkup: new ReplyKeyboardRemove());
            }
        }

        // Process Inline Keyboard callback data
        private static async Task BotOnCallbackQueryReceived(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message.Chat.Id,
                text: $"Received {callbackQuery.Data}");

            if (callbackQuery.Data =="Регистрация")
            {
                var tmp = new ForceReplyMarkup() { InputFieldPlaceholder = "emailforce" }; 
                var emailMessage = await botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                            text: "email", //не серым
                                                            replyMarkup: tmp);
                return;

            }
        }















        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }





        //private static async Task BotOnInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery)
        //{
        //    Console.WriteLine($"Received inline query from: {inlineQuery.From.Id}");

        //    InlineQueryResult[] results = {
        //    // displayed result
        //    new InlineQueryResultArticle(
        //        id: "3",
        //        title: "TgBots",
        //        inputMessageContent: new InputTextMessageContent(
        //            "hello"
        //        )
        //    )
        //};

        //    await botClient.AnswerInlineQueryAsync(inlineQueryId: inlineQuery.Id,
        //                                           results: results,
        //                                           isPersonal: true,
        //                                           cacheTime: 0);
        //}

        //static async Task<Telegram.Bot.Types.Message> SendReplyKeyboard(ITelegramBotClient botClient, Telegram.Bot.Types.Message message)
        //{
        //    ReplyKeyboardMarkup replyKeyboardMarkup = new(
        //        new[]
        //        {
        //            new KeyboardButton[] { "1.1", "1.2" },
        //            new KeyboardButton[] { "2.1", "2.2" },
        //        })
        //    {
        //        ResizeKeyboard = true
        //    };

        //    return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
        //                                                text: "Choose",
        //                                                replyMarkup: replyKeyboardMarkup);
        //}

        //static async Task<Telegram.Bot.Types.Message> RemoveKeyboard(ITelegramBotClient botClient, Telegram.Bot.Types.Message message)
        //{
        //    return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
        //                                                text: "Removing keyboard",
        //                                                replyMarkup: new ReplyKeyboardRemove());
        //}

        //private static Task BotOnChosenInlineResultReceived(ITelegramBotClient botClient, ChosenInlineResult chosenInlineResult)
        //{
        //    Console.WriteLine($"Received inline result: {chosenInlineResult.ResultId}");
        //    return Task.CompletedTask;
        //}
    }
}

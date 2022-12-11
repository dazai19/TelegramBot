using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp1.Command.Commands;
/// <summary>
/// Создание экранной клавиатуры
/// </summary>
public class KeyboardOn : Command
{
    public override string[] Names { get; set; } = new string[] { "/start", "start" };

    public override async void Execute(Message message, TelegramBotClient botClient)
    {
        ReplyKeyboardMarkup keyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "Download" }
        })
        {
            ResizeKeyboard = true
        };
        await botClient.SendTextMessageAsync(message.Chat.Id, $"Welcome, {message.Chat.FirstName ?? "no_name"}!",
            replyMarkup: keyboardMarkup);
    }
}
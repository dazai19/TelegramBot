using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleApp1.Command;
/// <summary>
/// Поиск команды
/// </summary>
public abstract class Command
{
    public abstract string[] Names { get; set; }
    /// <param name="message">текст сообщения</param>
    /// <returns>true/false</returns>
    public bool Contains(string message)
    {
        foreach (var mess in Names)
        {
            if (message.Contains(mess))
            {
                return true;
            }
        }

        return false;
    }

    public abstract void Execute(Message message, TelegramBotClient botClient);
}
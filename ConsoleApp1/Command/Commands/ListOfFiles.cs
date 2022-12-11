using System.Collections;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

namespace ConsoleApp1.Command.Commands;
/// <summary>
/// Создание кнопок с файлами в чате
/// </summary>
public class ListOfFiles : Command
{
    public override string[] Names { get; set; } = new string[] { "/download", "download" };

    public override async void Execute(Message message, TelegramBotClient botClient)
    {
        var uploadPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{message.Chat.Id}";
        if (Directory.Exists(uploadPath))
        {
            IEnumerable<string> pathFiles = Directory.EnumerateFiles(uploadPath, "*", SearchOption.AllDirectories);
            if (pathFiles.Any())
            {
                List<InlineKeyboardButton[]> list = new List<InlineKeyboardButton[]>();
                foreach (var file in pathFiles)
                {
                    string name = Path.GetFileName(file);
                    if (name.Length > 25) 
                    {
                        string oldName = name;
                        int dot = name.LastIndexOf(".");
                        name = name.Remove(20, dot - 20);
                        string newName = file.Substring(0, file.IndexOf(oldName));
                        File.Move(file, newName + name);
                        Console.WriteLine($"Rename: {oldName} -> {name}");
                    }
                    InlineKeyboardButton button = new InlineKeyboardButton(name) { CallbackData = name };
                    InlineKeyboardButton[] row = new InlineKeyboardButton[] { button };
                    list.Add(row);
                }

                InlineKeyboardMarkup keyboardMarkup = new InlineKeyboardMarkup(list);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Select file:\n", replyMarkup: keyboardMarkup);
            }
        }
        else
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, $"You don't have any files ^-^");
        }
    }
}
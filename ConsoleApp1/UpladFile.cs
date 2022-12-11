using System.Collections;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleApp1;

/// <summary>
/// Отправка файла
/// </summary>
public class UpladFile
{
    public async Task DownloadFileMe(TelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        var uploadPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{callbackQuery.Message.Chat.Id}";
        IEnumerable<string> pathFiles = Directory.EnumerateFiles(uploadPath, "*", SearchOption.AllDirectories);
        
        Hashtable fileList = new Hashtable();
        foreach (var file in pathFiles)
        {
            string name = Path.GetFileName(file);
            fileList.Add(name, file);
        }

        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Sending: {callbackQuery.Data}");
        await using Stream stream = System.IO.File.OpenRead((string)fileList[callbackQuery.Data]);
        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputFile(stream, callbackQuery.Data));
    }
}
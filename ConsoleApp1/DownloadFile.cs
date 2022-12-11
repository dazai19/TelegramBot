using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace ConsoleApp1;
/// <summary>
/// Загрузка отправленных файлов
/// </summary>
public class DownloadFile
{
    public async Task DownloadFileMe(Message message, TelegramBotClient botClient)
    {
        var chatId = message.Chat.Id;
        string downloadPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{chatId}";

        if (message.Document is not null)
        {
            Console.WriteLine($"{message.Chat.Username ?? "no name"} sent a document");

            var fileId = message.Document.FileId;
            var fileInfo = await botClient.GetFileAsync(fileId);
            var filePath = fileInfo.FilePath;
            string dirFileName = $@"{downloadPath}\document\{message.Document.FileName}";

            if (!Directory.Exists($@"{downloadPath}\document"))
            {
                Directory.CreateDirectory($@"{downloadPath}\document");
            }

            await using Stream fileStream = File.OpenWrite(dirFileName);
            await botClient.DownloadFileAsync(filePath, fileStream);
            fileStream.Close();

            await botClient.SendTextMessageAsync(chatId, "Document download");
        }

        if (message.Photo is not null)
        {
            Console.WriteLine($"{message.Chat.Username ?? "no name"} sent a photo");

            var fileId = message.Photo.Last().FileId;
            var fileInfo = await botClient.GetFileAsync(fileId);
            var filePath = fileInfo.FilePath;
            string destinationFilePath = $@"{downloadPath}\{filePath}";

            if (!Directory.Exists($@"{downloadPath}\photos"))
            {
                Directory.CreateDirectory($@"{downloadPath}\photos");
            }

            await using Stream fileStream = File.OpenWrite(destinationFilePath);
            await botClient.DownloadFileAsync(filePath, fileStream);
            fileStream.Close();

            await botClient.SendTextMessageAsync(chatId, "Photo download");
        }

        if (message.Audio is not null)
        {
            Console.WriteLine($"{message.Chat.Username ?? "no name"} sent a audio");

            var fileId = message.Audio.FileId;
            var fileInfo = await botClient.GetFileAsync(fileId);
            var filePath = fileInfo.FilePath;
            string dirFileName = $@"{downloadPath}\audio\{message.Audio.FileName}";

            if (!Directory.Exists($@"{downloadPath}\audio"))
            {
                Directory.CreateDirectory($@"{downloadPath}\audio");
            }

            await using Stream fileStream = File.OpenWrite(dirFileName);
            await botClient.DownloadFileAsync(filePath, fileStream);
            fileStream.Close();
            await botClient.SendTextMessageAsync(chatId, "Audio download");
        }
    }
}
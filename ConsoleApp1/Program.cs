using ConsoleApp1;
using ConsoleApp1.Command;
using ConsoleApp1.Command.Commands;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

DownloadFile downloadFile = new DownloadFile();
UpladFile upladFile = new UpladFile();

string token = Config.GetToken();
var client = new TelegramBotClient(token);
var me = await client.GetMeAsync();

List<Command> commands = new List<Command>();
commands.Add(new KeyboardOn());
commands.Add(new ListOfFiles());


Console.WriteLine($"Bot: {me.Username} enabled");
client.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync);
Console.ReadLine();
Console.WriteLine($"Bot: {me.Username} disabled");

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    var message = update.Message;

    if (update.Type == UpdateType.Message && message != null)
    {
        if (message.Document != null || message.Photo != null || message.Audio != null)
        {
            await downloadFile.DownloadFileMe(message, client);
        }

        if (message.Text != null)
        {
            Console.WriteLine($"{message.Chat.Username ?? "no_name"} send message");
            foreach (var comm in commands)
            {
                if (comm.Contains(message.Text.ToLower()))
                {
                    comm.Execute(message, client);
                }
            }
        }
    }

    if (update.Type == UpdateType.CallbackQuery)
    {
        if (update.CallbackQuery != null) await upladFile.DownloadFileMe(client, update.CallbackQuery);
    }
}

static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken arg3)
{
    string errorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Error telegram API:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
        _ => exception.ToString()
    };
    Console.WriteLine(errorMessage);
    return Task.CompletedTask;
}
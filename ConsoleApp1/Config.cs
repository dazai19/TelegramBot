namespace ConsoleApp1;
/// <summary>
/// Считывание токена бота из файла
/// </summary>
public abstract class Config
{
    private static string PathFile { get; } =
        $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\bot_key.txt";

    /// <returns>token</returns>
    public static string GetToken()
    {
        using var reader = new StreamReader(PathFile);
        var token = reader.ReadToEnd();
        return token;
    }
}
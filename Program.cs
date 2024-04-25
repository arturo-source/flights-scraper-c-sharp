var token = Environment.GetEnvironmentVariable("TG_TOKEN");
var chatIdStr = Environment.GetEnvironmentVariable("TG_CHAT_ID");

if (chatIdStr == null || token == null)
{
    Console.WriteLine("TG_TOKEN and TG_CHAT_ID are not set");
    return -1;
}

var chatId = int.Parse(chatIdStr);
var bot = new Telegram.Bot(token, chatId);
var resp = await bot.SendMessage("Hello world from C#");

Console.WriteLine(resp.ok);
Console.WriteLine(resp.error_code);
Console.WriteLine(resp.description);

return 0;

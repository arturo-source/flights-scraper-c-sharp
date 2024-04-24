var token = Environment.GetEnvironmentVariable("TG_TOKEN");
var chatIdStr = Environment.GetEnvironmentVariable("TG_CHAT_ID");

if (chatIdStr == null || token == null)
{
    Console.WriteLine("TG_TOKEN and TG_CHAT_ID are not set");
    return -1;
}

var chatId = int.Parse(chatIdStr);
var bot = new TelegramBot(token, chatId);
await bot.SendMessage("Hello world from C#");

return 0;

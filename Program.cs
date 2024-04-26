// var token = Environment.GetEnvironmentVariable("TG_TOKEN");
// var chatIdStr = Environment.GetEnvironmentVariable("TG_CHAT_ID");

// if (chatIdStr == null || token == null)
// {
//     Console.WriteLine("TG_TOKEN and TG_CHAT_ID are not set");
//     return -1;
// }

// var chatId = int.Parse(chatIdStr);
// var bot = new Telegram.Bot(token, chatId);
// var resp = await bot.SendMessage("Hello world from C#");

// if (resp == null)
// {
//     Console.WriteLine("response from SendTelegram is null");
//     return -1;
// }

// Console.WriteLine(resp.Ok);
// Console.WriteLine(resp.Error_code);
// Console.WriteLine(resp.Description);

var ryanair = new Travel.RyanairScraper();
var info = await ryanair.GetFlightInfo();
Console.WriteLine(info);

return 0;

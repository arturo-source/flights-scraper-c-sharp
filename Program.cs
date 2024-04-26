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

// -----------------

// var origin = "FKB";
// var destination = "ALC";
// var date = new DateTime(2024, 08, 23);

// var ryanair = new Travel.RyanairScraper();
// var info = await ryanair.GetFlightInfo(origin, destination, date);
// Console.WriteLine(info);

// -----------------

var ryanair = new Travel.RyanairScraper();
var config = Config.Config.ReadFromFile("example.yml");

foreach (var day in config)
{
    Console.WriteLine($"{day.Day}:\n");
    foreach (var combinations in day.Combinations ?? [])
    {
        if (combinations.Origin != null && combinations.Destination != null)
        {
            var info = await ryanair.GetFlightInfo(combinations.Origin, combinations.Destination, day.Day);
            Console.WriteLine(info);
        }
    }
}

return 0;

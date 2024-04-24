using System.Web;

class TelegramBot(string token, int chatId)
{
    private readonly string Token = token;
    private readonly int ChatId = chatId;
    private readonly HttpClient client = new();

    public async Task SendMessage(string msg)
    {
        var msgEncoded = HttpUtility.UrlEncode(msg);

        try
        {
            var resp = await client.GetAsync($"https://api.telegram.org/bot{Token}/sendMessage?chat_id={ChatId}&text={msgEncoded}");
            resp.EnsureSuccessStatusCode();
            var body = await resp.Content.ReadAsStringAsync();

            Console.WriteLine(body);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Exception during the request: " + e.Message);
        }
    }
}
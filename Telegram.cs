using System.Net.Http.Json;
using System.Web;

namespace Telegram;

class Response
{
    public bool ok = false;
    public string error_code = "";
    public string description = "";
}

class Bot(string token, int chatId)
{
    private readonly string Token = token;
    private readonly int ChatId = chatId;
    private readonly HttpClient client = new();

    public async Task<Response?> SendMessage(string msg)
    {
        var tgResp = new Response();
        var msgEncoded = HttpUtility.UrlEncode(msg);

        try
        {
            var resp = await client.GetAsync($"https://api.telegram.org/bot{Token}/sendMessage?chat_id={ChatId}&text={msgEncoded}");
            return await resp.Content.ReadFromJsonAsync<Response>();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Exception during the request: " + e.Message);
        }

        return null;
    }
}
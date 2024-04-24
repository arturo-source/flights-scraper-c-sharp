var client = new HttpClient();

async Task DoRequest()
{
    try
    {
        var resp = await client.GetAsync("https://learn.microsoft.com/en-us/");
        resp.EnsureSuccessStatusCode();
        var body = await resp.Content.ReadAsStringAsync();

        Console.WriteLine(body);
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception caught: " + e.Message);
    }
}

await DoRequest();
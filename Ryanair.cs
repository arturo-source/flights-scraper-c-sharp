using System.Net.Http.Json;
using System.Text;

namespace Travel;

class RyanairScraper
{
    private readonly HttpClient client = new();

    private async Task<HttpResponseMessage> DoRequest()
    {
        var request = new HttpRequestMessage(new HttpMethod("GET"), "https://www.ryanair.com/api/booking/v4/es-es/availability?ADT=1&TEEN=0&CHD=0&INF=0&Origin=FKB&Destination=ALC&promoCode=&IncludeConnectingFlights=false&DateOut=2024-08-23&DateIn=&FlexDaysBeforeOut=0&FlexDaysOut=0&FlexDaysBeforeIn=0&FlexDaysIn=0&RoundTrip=false&ToUs=AGREED");
        request.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
        request.Headers.TryAddWithoutValidation("accept-language", "es-ES,es;q=0.8");
        request.Headers.TryAddWithoutValidation("cache-control", "no-cache");
        request.Headers.TryAddWithoutValidation("client", "desktop");
        request.Headers.TryAddWithoutValidation("client-version", "3.113.0");
        request.Headers.TryAddWithoutValidation("cookie", "rid=a15d9f49-2b7d-4c37-9a38-40b00e6f260e; STORAGE_PREFERENCES={\"STRICTLY_NECESSARY\":true,\"PERFORMANCE\":false,\"FUNCTIONAL\":false,\"TARGETING\":false,\"SOCIAL_MEDIA\":false,\"PIXEL\":false,\"__VERSION\":3}; mkt=/es/es/; fr-correlation-id=e7c9cd76-7af2-4615-8961-81433d17ce4e; RY_COOKIE_CONSENT=true; rid.sig=RrjjHNquuQZkrfRiOnO9aIGMzwWPkQaeL5Dt0p5OmP6UqFUIrJQGY6qMeczKlySZC4sYGbdw64ZDT9g++ZXMku3sdVmxq8A8KKVcOfXLi9+OCbbbXZTYpHBSRsLD6XwlxJN78bD8eKYzsApD7ZWn7hvC579Ldm7Ezyl/tI+yu+atwZHSA1627ef+uX4bbQs1LYMTQ9eSNi5cqoIXgvHxFyhK1sEYzALRlSl+lFirVKL6gt/fl+5w+7BEuxXYVmdH+5i6wMdUgpv+NvP8oDicDWQ38mrPIf4hJunB9aKCRgo4V7eC8Hn1+xtyCVNwhqHcfpC9W/rFzd5563a9g0EvO3PnILKIeYqpkz6hbE/g3++LklxvUDfFMFiWPaM1974BeBscfFMPps659qPZDPxEEWd9FwB2Pndz0xGpMRl7Nv6aa2JPrGPeyTLue7j7633ZiSOciun1k63qIC10GWPQJaW4jqf82CpcYMqlx3XkAIC85PV69DuQr/vVzFtx1toAua6xAH7D0AvH7mn1Hf12U0MlStOCKc4pjac7K8AagxazK39XBfVzmqWj3s0rkKNmtT4aoEGXPIo6+bmcAZ12AKzNERs3DA6sCAPRNSyi0AD5tTj8rztHEg7iGRFmUyeR1WgGxV5w5CivZW7MDBrDs6wt3OM+i5PlFHJA5pc4NcgbHrXd2qqhITY29m9kCgOnIR9G5z8IHYC5XjfTFk48xQkTeYYaR2HqCtRKVL7L8byUttmmTpwHvRBAnDsHyeT2j6s1BFQ9XdYSBdD9glcsy7OErgbI3BfKSgL8NN/RtyRj95JW8TIe/vw/oxJiS5DT6x9TckZTE3r31S9ucPIOuzJbCfbcRlgEOlp07sW1ACxcFDDj4tDku9fESbse1k5r; .AspNetCore.Session=CfDJ8EY8BM24RC1Ft6DKD7z%2B4jwJLW%2BZ2GsokkZ6MROn4ArEUINUuBg7Foe97MDLiKPOnMDlCBuSprDp0bpirahYXW1DEg7oc7S8asAvl0H7eN9377U6YIe1730n9upb6PJgmSorfa6SUXOKLjIgXU7RFEvfjitQV11ND02GapudA16n; myRyanairID=");
        request.Headers.TryAddWithoutValidation("pragma", "no-cache");
        request.Headers.TryAddWithoutValidation("priority", "u=1, i");
        request.Headers.TryAddWithoutValidation("sec-ch-ua", "\"Chromium\";v=\"124\", \"Brave\";v=\"124\", \"Not-A.Brand\";v=\"99\"");
        request.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
        request.Headers.TryAddWithoutValidation("sec-ch-ua-platform", "\"Linux\"");
        request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
        request.Headers.TryAddWithoutValidation("sec-fetch-mode", "cors");
        request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
        request.Headers.TryAddWithoutValidation("sec-gpc", "1");
        request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36");

        return await client.SendAsync(request);
    }

    public async Task<string> GetFlightInfo()
    {
        var resp = await DoRequest();
        var ryanairResp = await resp.Content.ReadFromJsonAsync<RyanairResp>();
        if (ryanairResp == null)
        {
            return "";
        }

        var tripsStr = new StringBuilder();
        foreach (var trip in ryanairResp.Trips ?? [])
        {
            var tripStr = new StringBuilder();
            tripStr.Append(trip.Originname);
            tripStr.Append($" ({trip.Origin}) ");
            tripStr.Append("-> ");
            tripStr.Append(trip.Destinationname);
            tripStr.Append($" ({trip.Destination}):");
            tripStr.Append('\n');

            // should be only 1 date, because of the request options [FlexDaysBeforeOut=0, FlexDaysOut=0]
            foreach (var tripOnDate in trip.Dates ?? [])
            {
                foreach (var flight in tripOnDate.Flights ?? [])
                {
                    tripStr.Append('\t');
                    tripStr.Append(flight.Flightnumber);
                    // should be only 1 fare, because of the request options [ADT=1]
                    tripStr.Append($" left {flight.Faresleft} at price {flight.Regularfare?.Fares?[0].Amount}€");
                    tripStr.Append('\n');
                }
            }

            tripsStr.AppendLine(tripStr.ToString());
        }

        return tripsStr.ToString();
    }
}

public class Date
{
    public DateTime Dateout { get; set; }
    public List<Flight>? Flights { get; set; }
}

public class Fare
{
    public string? Type { get; set; }
    public double Amount { get; set; }
    public int Count { get; set; }
    public bool Hasdiscount { get; set; }
    public double Publishedfare { get; set; }
    public int Discountinpercent { get; set; }
    public bool Haspromodiscount { get; set; }
    public double Discountamount { get; set; }
    public bool Hasbogof { get; set; }
}

public class Flight
{
    public int Faresleft { get; set; }
    public string? Flightkey { get; set; }
    public int Infantsleft { get; set; }
    public RegularFare? Regularfare { get; set; }
    public string? Operatedby { get; set; }
    public List<Segment>? Segments { get; set; }
    public string? Flightnumber { get; set; }
    public List<DateTime>? Time { get; set; }
    public List<DateTime>? Timeutc { get; set; }
    public string? Duration { get; set; }
}

public class RegularFare
{
    public string? Farekey { get; set; }
    public string? Fareclass { get; set; }
    public List<Fare>? Fares { get; set; }
}

public class RyanairResp
{
    public string? Termsofuse { get; set; }
    public string? Currency { get; set; }
    public int Currprecision { get; set; }
    public string? Routegroup { get; set; }
    public string? Triptype { get; set; }
    public string? Upgradetype { get; set; }
    public List<Trip>? Trips { get; set; }
    public DateTime Servertimeutc { get; set; }
}

public class Segment
{
    public int Segmentnr { get; set; }
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public string? Flightnumber { get; set; }
    public List<DateTime>? Time { get; set; }
    public List<DateTime>? Timeutc { get; set; }
    public string? Duration { get; set; }
}

public class Trip
{
    public string? Origin { get; set; }
    public string? Originname { get; set; }
    public string? Destination { get; set; }
    public string? Destinationname { get; set; }
    public string? Routegroup { get; set; }
    public string? Triptype { get; set; }
    public string? Upgradetype { get; set; }
    public List<Date>? Dates { get; set; }
}


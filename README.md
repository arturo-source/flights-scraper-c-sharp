# Flights scraper

I'm learning C# so I decided to do a small project that consists in scraping different travel companies, and get the prices of the flights, and finally send the results by telegram.

## Enviroment

I will use as my development enviroment:

- Linux
- VSCode
- Dotnet core
- C# Dev Kit (VSCode extension)

## Where to get the data

You can get Ryanair data from <https://www.ryanair.com/api/booking/v4/es-es/availability>.

## Step by step

First I do create the GitHub repository, and download this one in my local machine.

Then I execute the following commands: `dotnet new console` to create the basic program, `dotnet new gitignore` because I don't want to upload unnecessary files to GitHub.

Now I want to learn how to fetch data, so I need an HTTP client.

- I found [System.Net.Http](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-8.0) which seems that I need.
- **Note:** I realized that I cannot declare an `async void` function because you cannot do an `await` to void functions. The correct way to do this in C# is `async Task`.

Next step will be sending a message by telegram.

- I will separate this into a class because it is the common way to program in C#.
- I use an interpolation, which is done with `$` symbol.
- Perhaps, encode the message with [System.Web.HttpUtility](https://learn.microsoft.com/en-us/dotnet/api/system.web.httputility.urlencode?view=net-8.0), so the message contain valid characters.
- Then I need to read environment variables, because of the bot token and chat id. I can use [System.Environment](https://learn.microsoft.com/en-us/dotnet/api/system.environment.getenvironmentvariable?view=net-7.0).
- But environment variables are strings, and chatId is an integer. The way to convert strings into integers in C# is with `int.Parse`.
- **Important:** `Environment.GetEnvironmentVariable` return nullable strings, so I must check that variables are not null! Cool compiler.

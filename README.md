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

Now I want to learn how to fetch data, so I need an HTTP client. I found [System.Net.Http](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-8.0) which seems that I need.

**Note:** I realized that I cannot declare an `async void` function because you cannot do an `await` to void functions. The correct way to do this in C# is `async Task`.

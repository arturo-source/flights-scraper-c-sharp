# Flights scraper

I'm learning C# so I decided to do a small project that consists in scraping different travel companies, and get the prices of the flights, and finally send the results by telegram.

## Enviroment

I will use as my development enviroment:

- Linux
- VSCode
- Dotnet core
- C# Dev Kit (VSCode extension)

## Endpoints

Send telegram message by doing a Get request to <https://api.telegram.org/bot{Token}/sendMessage?chat_id={ChatId}&text={msgEncoded}>.

Get Ryanair data from <https://www.ryanair.com/api/booking/v4/es-es/availability>.

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

Next step is to read Telegram response, so I can print if the message did not arrive or any other errors sending the message.

- I decide to create the `Response` class, so I can parse the JSON.
- As Telegram.cs is growing, I consider a good decision to encapsulate everything into a namespace `Telegram`, so I could call `new Telegram.Bot(_,_)`.
- **Note:** Class properties must have `{ get; set; }`, to be assignable when reading a JSON.
- **Note 2:** Public class properties must start with uppercase.

The following step is making the call to the travel pages. Let's start with ryanair and after that add some abstractions.

- First let's do the request with the same parameters as the browser. Just the ones of the example (1 adult from Karlsruhe to Alicante on 2024-08-23).
- Then use <https://json2csharp.com/> to transform the JSON response to C# classes.
- I change the properties first character to upper case because of the compiler warnings.
- Then I have to transform into a readable string, this is what the user sees.
- To build the string I use StringBuilder from [System.Text](https://learn.microsoft.com/es-es/dotnet/api/system.text.stringbuilder?view=net-8.0).

Now I must parameterize the options like origin, destination or the day to left.

- I am not able to find an url query builder in the standard library, so I decided to build the url query with a [Dictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-8.0).
- Then GetFlightInfo receives the origin, the destination, and the date.
- Working with dates is easy, due to [System.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.date?view=net-8.0). Ryanair needs year-month-day format.

Next step is introducing the data externally from the program. The options are: scanning user input, introducing as a program arguments, and the best in that case in my opinion, reading from a file. I'll use YAML file to store the flights I want to scrape.

- [System.IO.File](https://learn.microsoft.com/es-es/dotnet/api/system.io.file?view=net-8.0) works pretty fine to read files.
- But I also want to read the config as YAML, so I cannot only use the standard library.
- There is a package called [YamlDotNet](https://github.com/aaubry/YamlDotNet) to read and save in YAML format. So the way to install it is running `dotnet add package YamlDotNet`.

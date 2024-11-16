using PriceHubble.Client;
using PriceHubble.Client.Valuations;
using PriceHubble.Client.ValueTypes;
using PriceHubble.Client.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace PriceHubbleDotNetExample;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("PriceHubble example started");

            var envName = Environment.GetEnvironmentVariable("Environment");

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter("*", LogLevel.Debug).AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<Program>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            var priceHubbleOptions = new PriceHubbleOptions();
            configuration.Bind("PriceHubble", priceHubbleOptions);

            if(string.IsNullOrWhiteSpace(priceHubbleOptions.Username))
            {
                Console.Write("Username:");
                priceHubbleOptions.Username = Console.ReadLine();
            }

            if(string.IsNullOrWhiteSpace(priceHubbleOptions.Password))
            {
                Console.Write("Password:");
                priceHubbleOptions.Password = Console.ReadLine();
            }

            IPriceHubbleClient priceHubbleClient = new PriceHubbleClient(loggerFactory.CreateLogger<PriceHubbleClient>(), priceHubbleOptions);

            var exampleValuationLightRequest = new ValuationLightRequest()
            {
                DealType = DealType.Rent,
                Property = new PropertyLight
                {
                    Location = new Location
                    {
                        Address = new Address
                        {
                            Street = "Windscheidstr.",
                            HouseNumber = "21",
                            PostCode = "10629",
                            City = "Berlin"
                        }
                    },
                    PropertyType = new PropertyType
                    {
                        Code = PropertyTypeCode.Apartment
                    },
                    BuildingYear = 1910,
                    LivingArea = 48
                },
                CountryCode = "DE"
            };

            var valuationLightResult = priceHubbleClient.ValuationLightAsync(exampleValuationLightRequest).Result;

            if(valuationLightResult.IsSuccess)
            {
                var valuationLightResponse = valuationLightResult.AsSuccess;

                PrintValuations(valuationLightResponse);
            }
            else
            {
                Console.WriteLine("Request failed");
            }

            Console.ReadLine();
        }
        catch (Exception exc)
        {
            Console.WriteLine("Exception during runtime: " + exc.ToString());
        }
    }

    private static void PrintValuations(ValuationLightResponse valuationLightResponse)
    {
        Console.WriteLine("Confidence: " + valuationLightResponse.Confidence);
        Console.WriteLine("Currency: " + valuationLightResponse.Currency);
        Console.WriteLine("ValueRange: " + valuationLightResponse.ValueRange);
    }
}

using PriceHubble.Client;
using PriceHubble.Client.Models.Valuations;
using PriceHubble.Client.Models.Dossiers;
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

            if (string.IsNullOrWhiteSpace(priceHubbleOptions.Username))
            {
                Console.Write("Username:");
                priceHubbleOptions.Username = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(priceHubbleOptions.Password))
            {
                Console.Write("Password:");
                priceHubbleOptions.Password = Console.ReadLine();
            }

            IPriceHubbleClient priceHubbleClient = new PriceHubbleClient(loggerFactory.CreateLogger<PriceHubbleClient>(), priceHubbleOptions);

            //TestValuationLight(priceHubbleClient);
            TestDossierCreationAndShare(priceHubbleClient);

            Console.ReadLine();
        }
        catch (Exception exc)
        {
            Console.WriteLine("Exception during runtime: " + exc.ToString());
        }
    }

    private static void TestValuationLight(IPriceHubbleClient priceHubbleClient)
    {
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

        if (valuationLightResult.IsSuccess)
        {
            var valuationLightResponse = valuationLightResult.AsSuccess;

            PrintValuations(valuationLightResponse);
        }
        else
        {
            Console.WriteLine("Request failed");
        }
    }

    private static void PrintValuations(ValuationLightResponse valuationLightResponse)
    {
        Console.WriteLine("Confidence: " + valuationLightResponse.Confidence);
        Console.WriteLine("Currency: " + valuationLightResponse.Currency);
        Console.WriteLine("ValueRange: " + valuationLightResponse.ValueRange);
    }

    private static async void TestDossierCreationAndShare(IPriceHubbleClient priceHubbleClient)
    {
        var dossierCreationRequest = new DossierCreationRequest()
        {
            Title = "Test dossier",
            Description = "Test dossier description",
            DealType = DealType.Sale,
            Property = new Property
            {
                Location = new Location
                {
                    Address = new Address
                    {
                        Street = "Windscheidstrasse",
                        HouseNumber = "21",
                        PostCode = "10629",
                        City = "Berlin"
                    },
                },
                PropertyType = new PropertyType
                {
                    Code = PropertyTypeCode.Apartment,
                },
                BuildingYear = 1910,
                LivingArea = 48.24M,
                NumberOfRooms = 2,
                NumberOfBathrooms = 2
            },
            CountryCode = "DE",
        };

        var dossierCreationResult = await priceHubbleClient.DossierCreationAsync(dossierCreationRequest);
        var dossierCreationResponse = dossierCreationResult.AsSuccess;

        var dossierValuationRequest = new DossierValuationRequest();

        var dossierValuationResult = await priceHubbleClient.DossierValuationAsync(
            dossierId: dossierCreationResponse!.Id!,
            request: dossierValuationRequest);
        var dossierValuationResponse = dossierValuationResult.AsSuccess;

        var dossierSharingRequest = new DossierSharingRequest(
            dossierId: dossierCreationResponse.Id,
            countryCode: "DE",
            locale: "de_DE");

        var dossierSharingResult = await priceHubbleClient.DossierSharingAsync(dossierSharingRequest);
        var dossierSharingResponse = dossierSharingResult.AsSuccess;

        Console.WriteLine("URL: " + dossierSharingResponse.Url);
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Currencies.Models.Rest;
using System.Text.Json;

namespace Nox.Reference.GetData.CliCommands;

public class CurrencyDataExtractCommand : ICliCommand
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CountryDataExtractCommand> _logger;

    public CurrencyDataExtractCommand(
        IConfiguration configuration,
        ILogger<CountryDataExtractCommand> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void Execute()
    {
        _logger.LogInformation("Getting currency data...");

        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var targetOutputPath = _configuration.GetValue<string>(ConfigurationConstants.TargetDataPathSettingName)!;
        var uriRestWorldCurrencies = _configuration.GetValue<string>(ConfigurationConstants.UriRestCurrencyFormatterCurrenciesSettingName)!;

        try
        {
            var currencyFormatterRestData = RestHelper.GetInternetContent(uriRestWorldCurrencies).Content!;

            var sourceFilePath = Path.Combine(sourceOutputPath, "Currencies");
            Directory.CreateDirectory(sourceFilePath);

            var targetFilePath = targetOutputPath;
            Directory.CreateDirectory(targetFilePath);

            // Save content
            File.WriteAllText(Path.Combine(sourceFilePath, "currencyFormatter.json"), currencyFormatterRestData);

            var deserializationOptions = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            var currencyData = JsonSerializer.Deserialize<Dictionary<string, CurrencyInfo>>(currencyFormatterRestData, deserializationOptions) ?? new();

            foreach (var currency in currencyData)
            {
                try
                {
                    currency.Value.IsoCode_ = currency.Key;
                    var worldCurrencyRestDataResponse = RestHelper.GetInternetContent(uriRestWorldCurrencies + currency.Value.IsoCode.ToLower() + ".json5");

                    if (worldCurrencyRestDataResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new Exception("Can't find file in worldCurrency repo.");
                    }
                    var worldCurrencyRestData = worldCurrencyRestDataResponse.Content!;

                    // Fix Ni-Vanuatu Vatu
                    if (currency.Key.Equals("VUV"))
                    {
                        worldCurrencyRestData = worldCurrencyRestData.Replace("\"majorValue\": \"\"", "\"majorValue\": 1");
                    }

                    // Save content
                    File.WriteAllText(Path.Combine(sourceFilePath, $"worldCurrency_{currency.Value.IsoCode}.json"), worldCurrencyRestData);
                    var worldCurrencyData = JsonSerializer.Deserialize<Dictionary<string, WorldCurrencyRestData>>(worldCurrencyRestData, deserializationOptions) ?? new();

                    currency.Value.IsoNumber_ = worldCurrencyData.First().Value.Iso.Number;
                    currency.Value.Banknotes_ = worldCurrencyData.First().Value.Banknotes;
                    currency.Value.Coins_ = worldCurrencyData.First().Value.Coins;
                    currency.Value.Units_ = worldCurrencyData.First().Value.Units;
                    currency.Value.Name_ = worldCurrencyData.First().Value.Name;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Warning! WorldData doesn't have key for currency '{Key}' from CurrencyFormatter. Error message: {Message}", currency.Key, ex.Message);
                }
            }

            // Store output
            var serializationOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            var outputContent = JsonSerializer.Serialize(
                currencyData.Select(x => x.Value).Cast<ICurrencyInfo>(),
                serializationOptions);

            File.WriteAllText(Path.Combine(targetFilePath, "Nox.Reference.Currencies.json"), outputContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Currencies.Models.Rest;
using System.Text.Json;

namespace Nox.Reference.GetData.CliCommands;

public class CultureDataExtractCommand : ICliCommand
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CultureDataExtractCommand> _logger;

    public CultureDataExtractCommand(
        IConfiguration configuration,
        ILogger<CultureDataExtractCommand> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void Execute()
    {
        _logger.LogInformation("Getting currency data...");

        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var targetOutputPath = _configuration.GetValue<string>(ConfigurationConstants.TargetDataPathSettingName)!;
        var uriRestCurrencyFormatterCurrencies = _configuration.GetValue<string>(ConfigurationConstants.UriRestCurrencyFormatterCurrenciesSettingName)!;
        var uriRestWorldCurrencies = _configuration.GetValue<string>(ConfigurationConstants.UriRestWorldCurrenciesSettingName)!;

        try
        {
            var currencyFormatterRestData = RestHelper.GetInternetContent(uriRestCurrencyFormatterCurrencies).Content!;

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

            var worldCurrencyRestDataResponse = RestHelper.GetInternetContent(uriRestWorldCurrencies);
            if (worldCurrencyRestDataResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Can't find file in worldCurrency repo.");
            }
            var worldCurrencyRestData = worldCurrencyRestDataResponse.Content!;

            // Save content
            File.WriteAllText(Path.Combine(sourceFilePath, $"worldCurrency.json"), worldCurrencyRestData);

            // Fix Ni-Vanuatu Vatu
            worldCurrencyRestData = worldCurrencyRestData.Replace("\"majorValue\": \"\"", "\"majorValue\": 1");

            var worldCurrencyData = JsonSerializer.Deserialize<Dictionary<string, WorldCurrencyRestData>>(worldCurrencyRestData, deserializationOptions) ?? new();

            foreach (var currency in currencyData)
            {
                currency.Value.IsoCode_ = currency.Key;

                if (!worldCurrencyData.TryGetValue(currency.Value.IsoCode.ToUpper(), out var worldCurrencyInfo))
                {
                    _logger.LogWarning("Warning! WorldData doesn't have key for currency '{Key}' from CurrencyFormatter.", currency.Key);
                    continue;
                }

                currency.Value.IsoNumber_ = worldCurrencyInfo.Iso.Number;
                currency.Value.Banknotes_ = worldCurrencyInfo.Banknotes;
                currency.Value.Coins_ = worldCurrencyInfo.Coins;
                currency.Value.Units_ = worldCurrencyInfo.Units;
                currency.Value.Name_ = worldCurrencyInfo.Name;
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
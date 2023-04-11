﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Common;
using Nox.Reference.Currencies.Models.Rest;
using Nox.Reference.Data;
using Nox.Reference.GetData.Helpers;
using System.Text.Json;

namespace Nox.Reference.GetData.DataSeeds;

public class CurrencyDataSeeder : INoxReferenceDataSeeder
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CurrencyDataSeeder> _logger;
    private readonly INoxReferenceSeed<ICurrencyInfo> _dataSeed;

    public CurrencyDataSeeder(
        IConfiguration configuration,
        ILogger<CurrencyDataSeeder> logger,
        INoxReferenceSeed<ICurrencyInfo> dataSeed)
    {
        _configuration = configuration;
        _logger = logger;
        _dataSeed = dataSeed;
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
                        throw new NoxDataExtractorException("Can't find file in worldCurrency repo.");
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

            var currencies = currencyData
                .Select(x => x.Value);

            _dataSeed.Seed(currencies);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;
using System.Text.Json;

namespace Nox.Reference.Data.World;

internal class CurrencyDataSeeder : NoxReferenceDataSeederBase<WorldDbContext, CurrencyInfo, Currency>
{
    private readonly IConfiguration _configuration;

    public CurrencyDataSeeder(
        IConfiguration configuration,
        IMapper mapper,
        WorldDbContext dbContext,
        ILogger<CurrencyDataSeeder> logger,
        NoxReferenceFileStorageService noxReferenceFileStorage)
        : base(dbContext, mapper, logger, noxReferenceFileStorage)
    {
        _configuration = configuration;
    }

    public override string TargetFileName => "Nox.Reference.Currencies.json";

    public override string DataFolderPath => "Currencies";

    protected override IEnumerable<CurrencyInfo> GetDataInfos()
    {
        var uriRestCurrencyFormatterCurrencies = _configuration.GetValue<string>(ConfigurationConstants.UriRestCurrencyFormatterCurrenciesSettingName)!;
        var uriRestWorldCurrencies = _configuration.GetValue<string>(ConfigurationConstants.UriRestWorldCurrenciesSettingName)!;

        var currencyFormatterRestData = RestHelper.GetInternetContent(uriRestCurrencyFormatterCurrencies).Content!;
        _fileStorageService.SaveContentToSource(currencyFormatterRestData, DataFolderPath, "currencyFormatter.json");

        var deserializationOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        var currencyData = JsonSerializer.Deserialize<Dictionary<string, CurrencyInfo>>(currencyFormatterRestData, deserializationOptions) ?? new();
        var worldCurrencyRestDataResponse = RestHelper.GetInternetContent(uriRestWorldCurrencies);
        if (worldCurrencyRestDataResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new NoxDataExtractorException("Can't find file in worldCurrency repo.");
        }
        var worldCurrencyRestData = worldCurrencyRestDataResponse.Content!;

        // Fix Ni-Vanuatu Vatu
        worldCurrencyRestData = worldCurrencyRestData.Replace("\"majorValue\": \"\"", "\"majorValue\": 1");

        var worldCurrencyData = JsonSerializer.Deserialize<Dictionary<string, WorldCurrencyRestData>>(worldCurrencyRestData, deserializationOptions) ?? new();

        foreach (var currency in currencyData)
        {
            currency.Value.IsoCode = currency.Key;

            if (!worldCurrencyData.TryGetValue(currency.Value.IsoCode.ToUpper(), out var worldCurrencyInfo))
            {
                _logger.LogWarning("Warning! WorldData doesn't have key for currency '{Key}' from CurrencyFormatter.", currency.Key);
                continue;
            }

            currency.Value.IsoNumber = worldCurrencyInfo.Iso.Number;
            currency.Value.Banknotes = worldCurrencyInfo.Banknotes;
            currency.Value.Coins = worldCurrencyInfo.Coins;
            currency.Value.Units = worldCurrencyInfo.Units;
            currency.Value.Name = worldCurrencyInfo.Name;
        }

        var currencyInfos =
            currencyData.Select(x => x.Value)
            .ToList();

        EnumGeneratorService.Generate(currencyInfos.Where(x => !string.IsNullOrWhiteSpace(x.Name)),
            x => x.Name
            .Replace(" ", "")
            .Replace("'", "")
            .Replace("-", "")
            , "World", "Currencies");

        return currencyInfos;
    }
}
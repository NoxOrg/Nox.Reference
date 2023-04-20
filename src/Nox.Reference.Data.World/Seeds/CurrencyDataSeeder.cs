using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World;
using System.Text.Json;

namespace Nox.Reference.Country.DataContext;

internal class CurrencyDataSeeder : INoxReferenceDataSeeder
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly WorldDbContext _dbContext;
    private readonly ILogger<CurrencyDataSeeder> _logger;

    public CurrencyDataSeeder(
        IConfiguration configuration,
        IMapper mapper,
        WorldDbContext dbContext,
        ILogger<CurrencyDataSeeder> logger)
    {
        _configuration = configuration;
        _mapper = mapper;
        _dbContext = dbContext;
        _logger = logger;
    }

    public void Seed()
    {
        var dataSet = _dbContext
           .Set<Currency>();

        if (dataSet.Any())
        {
            return;
        }

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

            foreach (var currency in currencyData)
            {
                try
                {
                    currency.Value.IsoCode = currency.Key;
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

                    currency.Value.IsoNumber = worldCurrencyData.First().Value.Iso.Number;
                    currency.Value.Banknotes = worldCurrencyData.First().Value.Banknotes;
                    currency.Value.Coins = worldCurrencyData.First().Value.Coins;
                    currency.Value.Units = worldCurrencyData.First().Value.Units;
                    currency.Value.Name = worldCurrencyData.First().Value.Name;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Warning! WorldData doesn't have key for currency '{Key}' from CurrencyFormatter. Error message: {Message}", currency.Key, ex.Message);
                }
            }

            var currencies = currencyData
                .Select(x => x.Value);

            var entities = _mapper.Map<IEnumerable<Currency>>(currencies);
            dataSet.AddRange(entities);

            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Countries;
using System.Text.Json;

namespace Nox.Reference.GetData.CliCommands;

public class CountryDataExtractCommand : ICliCommand
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CountryDataExtractCommand> _logger;

    public CountryDataExtractCommand(
        IConfiguration configuration,
        ILogger<CountryDataExtractCommand> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void Execute()
    {
        _logger.LogInformation("Getting country data...");

        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var targetOutputPath = _configuration.GetValue<string>(ConfigurationConstants.TargetDataPathSettingName)!;
        var uriRestCountries = _configuration.GetValue<string>(ConfigurationConstants.UriRestCountriesSettingName)!;
        try
        {
            var data = RestHelper.GetInternetContent(uriRestCountries).Content!;

            var sourceFilePath = Path.Combine(sourceOutputPath, "Countries");
            Directory.CreateDirectory(sourceFilePath);

            var targetFilePath = targetOutputPath;
            Directory.CreateDirectory(targetFilePath);

            // Fix empty dictionaries for 'currencies' from empty arrays
            var editedContent = data.Replace(@"""currencies"": [],", @"""currencies"": {},");

            // save content
            File.WriteAllText(Path.Combine(sourceFilePath, "restcountries.json"), editedContent);

            var countries = JsonSerializer.Deserialize<RestcountryCountryInfo[]>(editedContent) ?? Array.Empty<RestcountryCountryInfo>();
            
            FixData(countries);
            EnrichWithMappingData(sourceFilePath, countries);
            FixTranslation(_configuration, countries);

            // Store output
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            var outputContent = JsonSerializer.Serialize(countries
                .Where(c => !string.IsNullOrEmpty(c.NumericCode))
                .Cast<ICountryInfo>(),
                options);

            File.WriteAllText(Path.Combine(targetFilePath, "Nox.Reference.Countries.json"), outputContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    private void FixTranslation(IConfiguration configuration, RestcountryCountryInfo[] countries)
    {
        var iso3LanguageData = LanguageDataExtractCommand.GetLanguageIso639_3_Data(configuration);
        foreach (var country in countries)
        {
            var dictionaryCopy = country.NameTranslations_.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var countryTranslations = dictionaryCopy.Select(x => x.Key).ToList();

            foreach (var translationLanguage in countryTranslations)
            {
                var countryData = dictionaryCopy[translationLanguage];
                dictionaryCopy.Remove(translationLanguage);
                
                string? newKey;
                if (translationLanguage == "per")
                {
                    newKey = "fa";
                }
                else
                {
                    newKey = iso3LanguageData.FirstOrDefault(x => translationLanguage.Equals(x.Iso_639_2t))?.Iso_639_1;
                }

                if (string.IsNullOrWhiteSpace(newKey))
                {
                    _logger.LogWarning("Warning! Can't find a language iso 639-1 code for language '{translationLanguage}'.", translationLanguage);
                }
                else
                {
                    dictionaryCopy[newKey!] = countryData;
                }
            }

            dictionaryCopy.Add("en", new RestcountryNativeNameInfo
            {
                CommonName = country.Name,
                OfficialName = country.Name,
                Language = "English",
            });

            country.NameTranslations_ = dictionaryCopy;
        }
    }

    private static void EnrichWithMappingData(string sourceFilePath, RestcountryCountryInfo[] countries)
    {
        var isoAlpha2ToFipsMapping = JsonSerializer.Deserialize<Dictionary<string, string>>(
                        File.ReadAllText(Path.Combine(sourceFilePath, "static-iso2fips.json"))
                    );

        foreach (var country in countries)
        {
            // Add fips codes
            if (isoAlpha2ToFipsMapping?.TryGetValue(country.AlphaCode2, out var fips) ?? false)
            {
                country.FipsCode = fips;
            }

            MapLatLongIntoGeoCoordinates(country);
        }
    }

    private static void FixData(RestcountryCountryInfo[] countries)
    {
        // Edit germany
        var germany = countries.First(c => c.Code.Equals("DEU"));

        if (germany is not null && germany.VehicleInfo_ is not null)
        {
            germany.VehicleInfo_.InternationalRegistrationCodes = new string[] { "D" };
        }
    }

    private static void MapLatLongIntoGeoCoordinates(RestcountryCountryInfo country)
    {
        if (country.LatLong?.Count == 2)
        {
            country.GeoCoordinates.Latitude = country.LatLong[0];
            country.GeoCoordinates.Longitude = country.LatLong[1];
        }
        country.LatLong = null!;

        if (country.CapitalInfo_?.LatLong?.Count == 2)
        {
            country.CapitalInfo_.GeoCoordinates.Latitude = country.CapitalInfo_.LatLong[0];
            country.CapitalInfo_.GeoCoordinates.Longitude = country.CapitalInfo_.LatLong[1];
        }

        if (country.CapitalInfo_ != null)
        {
            country.CapitalInfo_.LatLong = null!;
        }
    }
}
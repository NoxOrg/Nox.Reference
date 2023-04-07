using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using Nox.Reference.Countries;
using Nox.Reference.GetData.Helpers;
using System.Text.Json;

namespace Nox.Reference.GetData.DataSeeds;

public class CountryDataSeeder : INoxReferenceDataSeeder
{
    private readonly IConfiguration _configuration;
    private readonly INoxReferenceSeed<ICountryInfo> _dataSeed;
    private readonly ILogger<CountryDataSeeder> _logger;

    public CountryDataSeeder(
        IConfiguration configuration,
        INoxReferenceSeed<ICountryInfo> dataSeed,
        ILogger<CountryDataSeeder> logger)
    {
        _configuration = configuration;
        _dataSeed = dataSeed;
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

            // Edit germany
            var germany = countries.First(c => c.Code.Equals("DEU"));

            if (germany is not null && germany.VehicleInfo1 is not null)
            {
                germany.VehicleInfo1.InternationalRegistrationCodes = new string[] { "D" };
            }

            // Add fips codes
            var isoAlpha2ToFipsMapping = JsonSerializer.Deserialize<Dictionary<string, string>>(
                File.ReadAllText(Path.Combine(sourceFilePath, "static-iso2fips.json"))
            );

            foreach (var country in countries)
            {
                if (isoAlpha2ToFipsMapping?.TryGetValue(country.AlphaCode2, out var fips) ?? false)
                {
                    country.FipsCode = fips;
                }

                MapLatLongIntoGeoCoordinates(country);
            }
            var filteredCountries = countries
                .Where(c => !string.IsNullOrEmpty(c.NumericCode))
                .Cast<ICountryInfo>();

            _dataSeed.Seed(filteredCountries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
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

        if (country.CapitalInfo1?.LatLong?.Count == 2)
        {
            country.CapitalInfo1.GeoCoordinates.Latitude = country.CapitalInfo1.LatLong[0];
            country.CapitalInfo1.GeoCoordinates.Longitude = country.CapitalInfo1.LatLong[1];
        }

        if (country.CapitalInfo1 != null)
        {
            country.CapitalInfo1.LatLong = null!;
        }
    }
}
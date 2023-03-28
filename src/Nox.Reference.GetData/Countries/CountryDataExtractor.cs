using Nox.Reference.Countries;
using System.Text.Json;

internal class CountryDataExtractor
{
#pragma warning disable S1075 // URIs should not be hardcoded
    private const string uriRestCountries = @"https://gitlab.com/restcountries/restcountries/-/raw/master/src/main/resources/countriesV3.1.json";
#pragma warning restore S1075 // URIs should not be hardcoded

    internal static void GetRestCountryData(string sourceOutputPath, string targetOutputPath)
    {
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

            // Store output
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            var outputContent = System.Text.Json.JsonSerializer.Serialize(countries
                .Where(c => !string.IsNullOrEmpty(c.NumericCode))
                .Cast<ICountryInfo>(),
                options);

            File.WriteAllText(Path.Combine(targetFilePath, "Nox.Reference.Countries.json"), outputContent);
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
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
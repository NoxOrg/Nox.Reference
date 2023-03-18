using Nox.Reference.Countries;
using System.Text.Json;

internal class CountryDataExtractor
{
    private static string uriRestCountries = @"https://gitlab.com/restcountries/restcountries/-/raw/master/src/main/resources/countriesV3.1.json";

    internal static void GetRestCountryData(string sourceOutputPath, string targetOutputPath)
    {
        try
        {
            var data = RestHelper.GetInternetContent(uriRestCountries);

            var sourceFilePath = Path.Combine(sourceOutputPath, "Countries");
            Directory.CreateDirectory(sourceFilePath);

            var targetFilePath = targetOutputPath;
            Directory.CreateDirectory(targetFilePath);

            // Fix empty dictionaries for 'currencies' from empty arrays
            var editedContent = data.Replace(@"""currencies"": [],", @"""currencies"": {},");

            // save content
            File.WriteAllText(Path.Combine(sourceFilePath, "restcountries.json"), editedContent);

            RestcountryCountryInfo[] countries = JsonSerializer.Deserialize<RestcountryCountryInfo[]>(editedContent) ?? Array.Empty<RestcountryCountryInfo>();

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
            }

            // Store output
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,

            };

            var outputContent = JsonSerializer.Serialize(
                countries
                .Where(c => !string.IsNullOrEmpty(c.NumericCode))
                .Cast<ICountryInfo>()
            , options);

            File.WriteAllText(Path.Combine(targetFilePath, "Nox.Reference.Countries.json"), outputContent);

        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }
}
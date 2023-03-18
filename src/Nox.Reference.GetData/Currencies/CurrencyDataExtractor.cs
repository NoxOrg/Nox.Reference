using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Currencies.Models.Rest;
using System.Text.Json;

internal class CurrencyDataExtractor
{
    private static string uriRestWorldCurrencies = @"https://raw.githubusercontent.com/wiredmax/world-currencies/master/src/";
    private static string uriRestCurrencyFormatterCurrencies = @"https://raw.githubusercontent.com/smirzaei/currency-formatter/master/currencies.json";

    internal static void GetRestCurrencyData(string sourceOutputPath, string targetOutputPath)
    {
        try
        {
            var currencyFormatterRestData = RestHelper.GetInternetContent(uriRestCurrencyFormatterCurrencies).Content;

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
                    var worldCurrencyRestData = worldCurrencyRestDataResponse.Content;

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
                    Console.WriteLine($"Warning! WorldData doesn't have key for currency '{currency.Key}' from CurrencyFormatter. Error message: {ex.Message}");
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
            Console.Write(ex.Message);
        }
    }
}
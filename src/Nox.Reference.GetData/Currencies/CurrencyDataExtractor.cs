using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Currencies.Models.Rest;
using System.Text.Json;

internal class CurrencyDataExtractor
{
    private static string uriRestWorldCurrencies = @"https://github.com/wiredmax/world-currencies/blob/master/src/uah.json5";
    private static string uriRestCurrencyFormatterCurrencies = @"https://github.com/smirzaei/currency-formatter/blob/master/currencies.json";

    internal static void GetRestCurrencyData(string sourceOutputPath, string targetOutputPath)
    {
        try
        {
            var worldCurrencyRestData = RestHelper.GetInternetContent(uriRestWorldCurrencies);
            var currencyFormatterRestData = RestHelper.GetInternetContent(uriRestCurrencyFormatterCurrencies);

            var sourceFilePath = Path.Combine(sourceOutputPath, "Currencies");
            Directory.CreateDirectory(sourceFilePath);

            var targetFilePath = targetOutputPath;
            Directory.CreateDirectory(targetFilePath);

            // save content
            File.WriteAllText(Path.Combine(sourceFilePath, "currencyFormatter.json"), currencyFormatterRestData);
            File.WriteAllText(Path.Combine(sourceFilePath, "worldCurrency.json"), worldCurrencyRestData);

            var currencyData = JsonSerializer.Deserialize<Dictionary<string, CurrencyInfo>>(currencyFormatterRestData) ?? new();
            foreach (var currency in currencyData)
            {
                currency.Value.IsoCode_ = currency.Key;
            }

            var worldCurrencyData = JsonSerializer.Deserialize<Dictionary<string, WorldCurrencyRestData>>(worldCurrencyRestData) ?? new();
            foreach (var worldCurrency in worldCurrencyData)
            {
                if (!currencyData.TryGetValue(worldCurrency.Key, out var currency))
                {
                    Console.WriteLine($"Warning! CurrencyFormatter data doesn't have key for currency '{worldCurrency.Key}' from worldData!");
                    continue;
                }

                currency.IsoNumber_ = worldCurrency.Value.Iso.Number;
                currency.Banknotes_ = worldCurrency.Value.Banknotes;
                currency.Coins_ = worldCurrency.Value.Coins;
                currency.Units_ = worldCurrency.Value.Units;
                currency.Name_ = worldCurrency.Value.Name;
            }

            foreach (var currency in currencyData.Where(x => string.IsNullOrWhiteSpace(x.Value.IsoNumber)))
            {
                Console.WriteLine($"Warning! WorldData doesn't have key for currency '{currency.Key}' from CurrencyFormatter!");
            }

            // Store output
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,

            };

            var outputContent = JsonSerializer.Serialize(
                currencyData.Cast<ICurrencyInfo>(),
                options);

            File.WriteAllText(Path.Combine(targetFilePath, "Nox.Reference.Currencies.json"), outputContent);

        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }
}
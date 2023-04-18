using System.Text.Json.Serialization;

namespace Nox.Reference.Country.DataContext;

public class WorldCurrencyRestData
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("iso")] public RestIsoData Iso { get; set; } = new RestIsoData();
    [JsonPropertyName("units")] public CurrencyUnitInfo Units { get; set; } = new CurrencyUnitInfo();
    [JsonPropertyName("banknotes")] public CurrencyUsageInfo Banknotes { get; set; } = new CurrencyUsageInfo();
    [JsonPropertyName("coins")] public CurrencyUsageInfo Coins { get; set; } = new CurrencyUsageInfo();
}
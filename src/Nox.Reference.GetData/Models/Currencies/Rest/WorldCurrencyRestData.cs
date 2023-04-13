using System.Text.Json.Serialization;

namespace Nox.Reference.GetData.Models.Rest;

public class WorldCurrencyRestData
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("iso")] public RestIsoData Iso { get; set; } = new RestIsoData();
    [JsonPropertyName("units")] public CurrencyUnit Units { get; set; } = new CurrencyUnit();
    [JsonPropertyName("banknotes")] public CurrencyUsage Banknotes { get; set; } = new CurrencyUsage();
    [JsonPropertyName("coins")] public CurrencyUsage Coins { get; set; } = new CurrencyUsage();
}
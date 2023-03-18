using Nox.Reference.Abstractions.Currencies;
using System.Text.Json.Serialization;

namespace Nox.Reference.Currencies.Models.Rest
{
    public class WorldCurrencyRestData
    {
        [JsonPropertyName("name")] public string Name { get; set; } = null;
        [JsonPropertyName("iso")] public RestIsoData Iso { get; set; } = null;
        [JsonPropertyName("units")] public CurrencyUnit Units { get; set; } = null;
        [JsonPropertyName("banknotes")] public CurrencyUsage Banknotes { get; set; } = null;
        [JsonPropertyName("coins")] public CurrencyUsage Coins { get; set; } = null;
    }
}

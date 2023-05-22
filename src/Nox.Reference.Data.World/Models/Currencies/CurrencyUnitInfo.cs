using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

internal class CurrencyUnitInfo
{
    [JsonPropertyName("major")]
    public MajorCurrencyUnitInfo MajorCurrencyUnit { get; set; } = new MajorCurrencyUnitInfo();

    [JsonPropertyName("minor")]
    public MinorCurrencyUnitInfo MinorCurrencyUnit { get; set; } = new MinorCurrencyUnitInfo();
}
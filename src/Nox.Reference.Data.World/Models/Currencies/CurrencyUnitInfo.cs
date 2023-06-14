using System.Text.Json.Serialization;

namespace Nox.Reference;

public class CurrencyUnitInfo
{
    [JsonPropertyName("major")]
    public MajorCurrencyUnitInfo MajorCurrencyUnit { get; set; } = new MajorCurrencyUnitInfo();

    [JsonPropertyName("minor")]
    public MinorCurrencyUnitInfo MinorCurrencyUnit { get; set; } = new MinorCurrencyUnitInfo();
}
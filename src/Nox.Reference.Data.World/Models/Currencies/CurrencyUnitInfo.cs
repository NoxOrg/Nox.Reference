using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Country.DataContext;

internal class CurrencyUnitInfo : ICurrencyUnit
{
    [JsonPropertyName("major")]
    public IMajorCurrencyUnit MajorCurrencyUnit { get; set; } = new MajorCurrencyUnitInfo();

    [JsonPropertyName("minor")]
    public IMinorCurrencyUnit MinorCurrencyUnit { get; set; } = new MinorCurrencyUnitInfo();
}
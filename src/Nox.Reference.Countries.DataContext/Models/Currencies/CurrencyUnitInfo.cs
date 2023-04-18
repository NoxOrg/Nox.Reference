using System.Text.Json.Serialization;
using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Country.DataContext;

public class CurrencyUnitInfo : ICurrencyUnit
{
    [JsonPropertyName("major")] public MajorCurrencyUnitInfo MajorCurrencyUnit_ { get; set; } = new MajorCurrencyUnitInfo();
    [JsonPropertyName("minor")] public MinorCurrencyUnitInfo MinorCurrencyUnit_ { get; set; } = new MinorCurrencyUnitInfo();

    [JsonIgnore] public IMajorCurrencyUnit MajorCurrencyUnit => MajorCurrencyUnit_;
    [JsonIgnore] public IMinorCurrencyUnit MinorCurrencyUnit => MinorCurrencyUnit_;
}
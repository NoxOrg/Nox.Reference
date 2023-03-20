using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions.Currencies;

public class CurrencyUnit : ICurrencyUnit
{
    [JsonPropertyName("major")] public MajorCurrencyUnit MajorCurrencyUnit_ { get; set; } = null;
    [JsonPropertyName("minor")] public MinorCurrencyUnit MinorCurrencyUnit_ { get; set; } = null;

    [JsonIgnore] public IMajorCurrencyUnit MajorCurrencyUnit => MajorCurrencyUnit_;
    [JsonIgnore] public IMinorCurrencyUnit MinorCurrencyUnit => MinorCurrencyUnit_;
}
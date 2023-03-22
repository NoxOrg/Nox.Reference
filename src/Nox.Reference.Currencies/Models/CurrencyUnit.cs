using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions.Currencies;

public class CurrencyUnit : ICurrencyUnit
{
    [JsonPropertyName("major")] public MajorCurrencyUnit MajorCurrencyUnit_ { get; set; } = new MajorCurrencyUnit();
    [JsonPropertyName("minor")] public MinorCurrencyUnit MinorCurrencyUnit_ { get; set; } = new MinorCurrencyUnit();

    [JsonIgnore] public IMajorCurrencyUnit MajorCurrencyUnit => MajorCurrencyUnit_;
    [JsonIgnore] public IMinorCurrencyUnit MinorCurrencyUnit => MinorCurrencyUnit_;
}
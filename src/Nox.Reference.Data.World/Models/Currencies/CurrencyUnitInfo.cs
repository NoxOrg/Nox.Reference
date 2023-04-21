using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;
using Nox.Reference.Common;

namespace Nox.Reference.Data.World;

internal class CurrencyUnitInfo : ICurrencyUnit
{
    [JsonPropertyName("major")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IMajorCurrencyUnit, MajorCurrencyUnitInfo>))]
    public IMajorCurrencyUnit MajorCurrencyUnit { get; set; } = new MajorCurrencyUnitInfo();

    [JsonPropertyName("minor")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IMinorCurrencyUnit, MinorCurrencyUnitInfo>))]
    public IMinorCurrencyUnit MinorCurrencyUnit { get; set; } = new MinorCurrencyUnitInfo();
}
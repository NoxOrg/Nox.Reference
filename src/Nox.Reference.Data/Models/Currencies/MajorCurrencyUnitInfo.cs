using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Data;

internal class MajorCurrencyUnitInfo : IMajorCurrencyUnit
{
    public string Name { get; set; }
    public string Symbol { get; set; }
}
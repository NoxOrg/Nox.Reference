using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Data;

internal class MinorCurrencyUnitInfo : IMinorCurrencyUnit
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public decimal MajorValue { get; set; }
}
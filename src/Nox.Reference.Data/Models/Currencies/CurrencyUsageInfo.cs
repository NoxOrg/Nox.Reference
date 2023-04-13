using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Data;

internal class CurrencyUsageInfo : ICurrencyUsage
{
    public IReadOnlyList<string> Frequent { get; set; }
    public IReadOnlyList<string> Rare { get; set; }
}
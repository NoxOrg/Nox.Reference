
namespace Nox.Reference.Abstractions.Currencies;

public interface ICurrencyUsage
{
    public IReadOnlyList<string> Frequent { get; }
    public IReadOnlyList<string> Rare { get; }
}
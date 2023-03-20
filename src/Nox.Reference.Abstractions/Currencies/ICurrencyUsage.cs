
namespace Nox.Reference.Abstractions.Currencies;

public interface ICurrencyUsage
{
    public List<string> Frequent { get; }
    public List<string> Rare { get; }
}
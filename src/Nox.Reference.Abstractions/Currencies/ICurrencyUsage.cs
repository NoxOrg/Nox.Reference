
namespace Nox.Reference.Abstractions;

public interface ICurrencyUsage
{
    public IReadOnlyList<string> Frequent { get; }
    public IReadOnlyList<string> Rare { get; }
}
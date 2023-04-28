using Nox.Reference.Abstractions;
using Nox.Reference.Abstractions.Cultures;

namespace Nox.Reference.Data.World;

public interface IWorldInfoContext
{
    IQueryable<ICurrencyInfo> Currencies { get; }
    IQueryable<ICultureInfo> Cultures { get; }
}
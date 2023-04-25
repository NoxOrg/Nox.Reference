using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public interface IWorldInfoContext
{
    IQueryable<ICurrencyInfo> Currencies { get; }
    IQueryable<IVatNumberDefinitionInfo> VatNumberDefinitions { get; }
}
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Nox.Reference.Data.World.Mappings;

internal class CurrencyMappingResolver : SourceArrayToEntitiesMappingResolverBase<string, Currency>
{
    public CurrencyMappingResolver(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger<CurrencyMappingResolver> logger)
        : base(worldDbContext, mapper, logger)
    {
    }

    protected override Expression<Func<Currency, bool>> KeySearchExpression(string source)
        => x => x.IsoCode == source;
}

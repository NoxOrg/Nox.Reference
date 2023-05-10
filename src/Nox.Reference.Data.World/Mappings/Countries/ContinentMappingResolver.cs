using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Nox.Reference.Data.World.Mappings;

internal class ContinentMappingResolver : SourceArrayToEntitiesMappingResolverBase<string, Continent>
{
    public ContinentMappingResolver(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger<ContinentMappingResolver> logger) : base(worldDbContext, mapper, logger)
    {
    }

    protected override Expression<Func<Continent, bool>> KeySearchExpression(string source)
         => x => x.Name == source;
}

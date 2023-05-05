using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Nox.Reference.Data.World.Mappings;

internal class TopLevelDomainMappingResolver : SourceArrayToEntitiesMappingResolverBase<string, TopLevelDomain>
{
    public TopLevelDomainMappingResolver(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger<TopLevelDomainMappingResolver> logger) : base(worldDbContext, mapper, logger)
    {
    }

    protected override Expression<Func<TopLevelDomain, bool>> KeySearchExpression(string source)
         => x => x.Name == source;
}

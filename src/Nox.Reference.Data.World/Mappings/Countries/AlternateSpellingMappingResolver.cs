using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Nox.Reference.Data.World.Mappings;

internal class AlternateSpellingMappingResolver : SourceArrayToEntitiesMappingResolverBase<string, AlternateSpelling>
{
    public AlternateSpellingMappingResolver(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger<AlternateSpellingMappingResolver> logger) : base(worldDbContext, mapper, logger)
    {
    }

    protected override Expression<Func<AlternateSpelling, bool>> KeySearchExpression(string source)
         => x => x.Name == source;
}

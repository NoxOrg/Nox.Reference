using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Nox.Reference.Data.World.Mappings;

internal class LanguageMappingResolver : SourceArrayToEntitiesMappingResolverBase<string, Language>
{
    public LanguageMappingResolver(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger<LanguageMappingResolver> logger)
        : base(worldDbContext, mapper, logger)
    {
    }

    protected override Expression<Func<Language, bool>> KeySearchExpression(string source)
        => x => x.Iso_639_3 == source;
}

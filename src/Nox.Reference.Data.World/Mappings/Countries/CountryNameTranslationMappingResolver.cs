using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Mappings;

internal class CountryNameTranslationMappingResolver : SourceArrayToEntitiesMappingResolverBase<INativeNameInfo, CountryNameTranslation>
{
    public CountryNameTranslationMappingResolver(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger<CountryNameTranslationMappingResolver> logger) : base(worldDbContext, mapper, logger)
    {
    }

    protected override Expression<Func<CountryNameTranslation, bool>> KeySearchExpression(INativeNameInfo source)
         => x => x.Language.Iso_639_1 == source.Language
         && x.OfficialName == source.OfficialName
         && x.CommonName == source.OfficialName;
}

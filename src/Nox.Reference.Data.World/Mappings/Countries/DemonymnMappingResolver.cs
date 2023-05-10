using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Mappings;

internal class DemonymnMappingResolver : SourceArrayToEntitiesMappingResolverBase<IDemonymn, Demonymn>
{
    public DemonymnMappingResolver(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger<DemonymnMappingResolver> logger) : base(worldDbContext, mapper, logger)
    {
    }

    protected override Expression<Func<Demonymn, bool>> KeySearchExpression(IDemonymn source)
         => x => x.Feminine == source.Feminine
         && x.Masculine == source.Masculine
         && x.Language.Iso_639_3 == source.Language;
}
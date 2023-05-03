using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Extensions;

namespace Nox.Reference.Data.World.Mappings;

internal class CountryMapping : Profile
{
    public CountryMapping()
    {
        CreateMap<string, TopLevelDomain>().ForMember(x => x.Name, x => x.MapFrom(t => t));
        CreateMap<string, AlternateSpelling>().ForMember(x => x.Name, x => x.MapFrom(t => t));
        CreateMap<string, Continent>().ForMember(x => x.Name, x => x.MapFrom(t => t));
        CreateMap<INativeNameInfo, CountryNameTranslation>().ConvertUsing<CountryNameTranslationSingleMapping>();

        CreateMap<IReadOnlyList<string>, IReadOnlyList<Language>>().ConvertUsing<LanguageMappingResolver>();
        CreateMap<IReadOnlyList<string>, IReadOnlyList<Currency>>().ConvertUsing<CurrencyMappingResolver>();
        CreateMap<IReadOnlyList<string>, IReadOnlyList<TopLevelDomain>>().ConvertUsing<TopLevelDomainMappingResolver>();
        CreateMap<IReadOnlyList<string>, IReadOnlyList<AlternateSpelling>>().ConvertUsing<AlternateSpellingMappingResolver>();
        CreateMap<IReadOnlyList<string>, IReadOnlyList<Continent>>().ConvertUsing<ContinentMappingResolver>();
        CreateMap<IReadOnlyList<INativeNameInfo>, IReadOnlyList<CountryNameTranslation>>().ConvertUsing<CountryNameTranslationMappingResolver>();

        CreateMap<ICountryInfo, Country>()
            .IgnoreAllMembers()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.TopLevelDomains, x => x.MapFrom(t => t.TopLevelDomains))
            .ForMember(x => x.Languages, x => x.MapFrom(t => t.Languages))
            .ForMember(x => x.Currencies, x => x.MapFrom(t => t.Currencies))
            .ForMember(x => x.AlternateSpellings, x => x.MapFrom(t => t.AlternateSpellings))
            .ForMember(x => x.Continents, x => x.MapFrom(t => t.Continents))
            .ForMember(x => x.NameTranslations, x => x.MapFrom(t => t.NameTranslations));
    }
}

internal abstract class SourceToEntiyMappingResolverBase<TSource, TEntity> : ITypeConverter<TSource, TEntity>
    where TEntity : class, INoxReferenceEntity
{
    protected readonly WorldDbContext _worldDbContext;
    private readonly IMapper _mapper;
    protected readonly ILogger _logger;

    protected SourceToEntiyMappingResolverBase(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger logger)
    {
        _worldDbContext = worldDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    protected abstract Expression<Func<TEntity, bool>> KeySearchExpression(TSource source);

    public TEntity Convert(TSource source, TEntity? destination, ResolutionContext context)
    {
        var dbSet = _worldDbContext
            .Set<TEntity>();

        try
        {
            var exp = KeySearchExpression(source);
            destination = dbSet.FirstOrDefault(exp);

            if (destination == null)
            {
                destination = _mapper.Map<TEntity>(source);
                dbSet.Add(destination);
                _worldDbContext.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurs during mapping {source}. Error:{error}", source, ex.Message);
        }

        return destination;
    }
}

internal class CountryNameTranslationSingleMapping : SourceToEntiyMappingResolverBase<INativeNameInfo, CountryNameTranslation>
{
    public CountryNameTranslationSingleMapping(WorldDbContext worldDbContext, IMapper mapper, ILogger<CountryNameTranslationSingleMapping> logger)
        : base(worldDbContext, mapper, logger)
    {
    }

    protected override Expression<Func<CountryNameTranslation, bool>> KeySearchExpression(INativeNameInfo source)
        => x => x.Language.Iso_639_1 == source.Language;
}

internal abstract class SourceArrayToEntitiesMappingResolverBase<TSource, TEntity> : ITypeConverter<IReadOnlyList<TSource>, IReadOnlyList<TEntity>>
    where TEntity : class, INoxReferenceEntity
{
    protected readonly WorldDbContext _worldDbContext;
    private readonly IMapper _mapper;
    protected readonly ILogger _logger;

    protected SourceArrayToEntitiesMappingResolverBase(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger logger)
    {
        _worldDbContext = worldDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    protected abstract Expression<Func<TEntity, bool>> KeySearchExpression(TSource source);

    public IReadOnlyList<TEntity> Convert(IReadOnlyList<TSource> source, IReadOnlyList<TEntity> destination, ResolutionContext context)
    {
        var items = new List<TEntity>();

        var dbSet = _worldDbContext
            .Set<TEntity>();

        foreach (var sourceItem in source)
        {
            try
            {
                var exp = KeySearchExpression(sourceItem);
                var entity = dbSet.FirstOrDefault(exp);

                if (entity == null)
                {
                    entity = _mapper.Map<TEntity>(sourceItem);
                    dbSet.Add(entity);
                    _worldDbContext.SaveChanges();
                }

                items.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurs during mapping {source}. Error:{error}", sourceItem, ex.Message);

                destination = Array.Empty<TEntity>();
            }
        }
        destination = items;

        return destination;
    }
}

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

internal class CountryNameTranslationMappingResolver : SourceArrayToEntitiesMappingResolverBase<INativeNameInfo, CountryNameTranslation>
{
    public CountryNameTranslationMappingResolver(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger<CountryNameTranslationMappingResolver> logger) : base(worldDbContext, mapper, logger)
    {
    }

    protected override Expression<Func<CountryNameTranslation, bool>> KeySearchExpression(INativeNameInfo source)
         => x => x.Language.Name == source.Language;
}
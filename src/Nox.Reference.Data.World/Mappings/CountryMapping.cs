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
        CreateMap<IDemonymn, Demonymn>().ConvertUsing<DemonymnSingleMapping>();
        CreateMap<ICoatOfArms, CoatOfArms>();
        CreateMap<IGeoCoordinates, GeoCoordinates>();
        CreateMap<IMaps, CountryMaps>();
        CreateMap<IFlags, CountryFlag>();
        CreateMap<IVehicleInfo, CountryVehicle>()
            .ForMember(x => x.InternationalRegistrationCodes, x => x.MapFrom(t => string.Join(",", t.InternationalRegistrationCodes)));

        CreateMap<IPostalCodeInfo, PostalCode>();
        CreateMap<IDialingInfo, CountryDialing>()
            .ForMember(x => x.Suffixes, x => x.MapFrom(t => string.Join(",", t.Suffixes)));

        CreateMap<INativeNameInfo, CountryNativeName>().ConvertUsing<CountryNativeNameSingleMapping>();
        CreateMap<ICountryNames, CountryNames>();

        CreateMap<IReadOnlyList<string>, IReadOnlyList<Language>>().ConvertUsing<LanguageMappingResolver>();
        CreateMap<IReadOnlyList<string>, IReadOnlyList<Currency>>().ConvertUsing<CurrencyMappingResolver>();
        CreateMap<IReadOnlyList<string>, IReadOnlyList<TopLevelDomain>>().ConvertUsing<TopLevelDomainMappingResolver>();
        CreateMap<IReadOnlyList<string>, IReadOnlyList<AlternateSpelling>>().ConvertUsing<AlternateSpellingMappingResolver>();
        CreateMap<IReadOnlyList<string>, IReadOnlyList<Continent>>().ConvertUsing<ContinentMappingResolver>();
        CreateMap<IReadOnlyList<IDemonymn>, IReadOnlyList<Demonymn>>().ConvertUsing<DemonymnMappingResolver>();
        CreateMap<IReadOnlyList<INativeNameInfo>, IReadOnlyList<CountryNameTranslation>>().ConvertUsing<CountryNameTranslationMappingResolver>();

        CreateMap<IReadOnlyDictionary<string, decimal>, IReadOnlyList<GiniCoefficient>>()
            .ConstructUsing(x => x.Keys.Select(t => new GiniCoefficient
            {
                Year = int.Parse(t),
                Value = x[t]
            }).ToList());

        CreateMap<ICountryInfo, Country>()
            //.IgnoreAllMembers()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.BorderingCountries, x => x.Ignore())
            //.ForMember(x => x.TopLevelDomains, x => x.MapFrom(t => t.TopLevelDomains))
            //.ForMember(x => x.Languages, x => x.MapFrom(t => t.Languages))
            //.ForMember(x => x.Currencies, x => x.MapFrom(t => t.Currencies))
            //.ForMember(x => x.AlternateSpellings, x => x.MapFrom(t => t.AlternateSpellings))
            //.ForMember(x => x.Continents, x => x.MapFrom(t => t.Continents))
            .ForMember(x => x.NameTranslations, x => x.Ignore())
            //.ForMember(x => x.GiniCoefficients, x => x.MapFrom(t => t.GiniCoefficients))
            //.ForMember(x => x.Demonyms, x => x.MapFrom(t => t.Demonyms))
            //.ForMember(x => x.CoatOfArms, x => x.MapFrom(t => t.CoatOfArms))
            //.ForMember(x => x.GeoCoordinates, x => x.MapFrom(t => t.GeoCoordinates))
            //.ForMember(x => x.Maps, x => x.MapFrom(t => t.Maps))
            .ForMember(x => x.Dialing, x => x.MapFrom(t => t.DialingInfo))
            .ForMember(x => x.Flag, x => x.MapFrom(t => t.Flags))
            //.ForMember(x => x.Names, x => x.MapFrom(t => t.Names))
            .ForMember(x => x.Vehicle, x => x.MapFrom(t => t.VehicleInfo))
            .ForMember(x => x.PostalCode, x => x.MapFrom(t => t.PostalCodeInfo))
            .ForMember(x => x.Capitals, x => x.MapFrom(t => t.Capitals.Select(s => new CountryCapital
            {
                Name = s,
                GeoCoordinates = (t.CapitalInfo == null ? null : new GeoCoordinates
                {
                    Latitude = t.GeoCoordinates.Latitude,
                    Longitude = t.GeoCoordinates.Longitude
                })
            }).ToList()));
    }
}

internal class CountryNameTranslationSingleMapping : ITypeConverter<INativeNameInfo, CountryNameTranslation>
{
    private readonly WorldDbContext _worldDbContext;
    private readonly ILogger<CountryNameTranslationSingleMapping> _logger;

    public CountryNameTranslationSingleMapping(
        WorldDbContext worldDbContext,
        ILogger<CountryNameTranslationSingleMapping> logger)
    {
        _worldDbContext = worldDbContext;
        _logger = logger;
    }

    public CountryNameTranslation Convert(INativeNameInfo source, CountryNameTranslation destination, ResolutionContext context)
    {
        var language = _worldDbContext
            .Set<Language>()
            .FirstOrDefault(x => x.Iso_639_1 == source.Language);

        if (language == null)
        {
            return null;
        }

        destination = new CountryNameTranslation
        {
            Language = language,
            CommonName = source.CommonName,
            OfficialName = source.OfficialName
        };

        return destination;
    }
}

internal class CountryNativeNameSingleMapping : ITypeConverter<INativeNameInfo, CountryNativeName>
{
    private readonly WorldDbContext _worldDbContext;
    private readonly ILogger<CountryNativeNameSingleMapping> _logger;

    public CountryNativeNameSingleMapping(
        WorldDbContext worldDbContext,
        ILogger<CountryNativeNameSingleMapping> logger)
    {
        _worldDbContext = worldDbContext;
        _logger = logger;
    }

    public CountryNativeName Convert(INativeNameInfo source, CountryNativeName destination, ResolutionContext context)
    {
        var language = _worldDbContext
            .Set<Language>()
            .FirstOrDefault(x => x.Iso_639_3 == source.Language);

        if (language == null)
        {
            return null;
        }

        destination = new CountryNativeName
        {
            Language = language,
            CommonName = source.CommonName,
            OfficialName = source.OfficialName
        };

        return destination;
    }
}

internal class DemonymnSingleMapping : ITypeConverter<IDemonymn, Demonymn>
{
    private readonly WorldDbContext _worldDbContext;
    private readonly ILogger<DemonymnSingleMapping> _logger;

    public DemonymnSingleMapping(
        WorldDbContext worldDbContext,
        ILogger<DemonymnSingleMapping> logger)
    {
        _worldDbContext = worldDbContext;
        _logger = logger;
    }

    public Demonymn Convert(IDemonymn source, Demonymn destination, ResolutionContext context)
    {
        var language = _worldDbContext
            .Set<Language>()
            .FirstOrDefault(x => x.Iso_639_3 == source.Language);

        if (language == null)
        {
            return null;
        }

        destination = new Demonymn
        {
            Language = language,
            Feminine = source.Feminine,
            Masculine = source.Masculine
        };

        return destination;
    }
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
         => x => x.Language.Iso_639_1 == source.Language
         && x.OfficialName == source.OfficialName
         && x.CommonName == source.OfficialName;
}

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
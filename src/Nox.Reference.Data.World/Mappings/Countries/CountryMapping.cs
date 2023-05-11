using AutoMapper;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Mappings;

internal class CountryMapping : Profile
{
    public CountryMapping()
    {
        //Write
        MapCountryInfoToCountry();

        //Read
        MapCountryToCountryInfo();
    }

    private void MapCountryToCountryInfo()
    {
        CreateMap<Demonymn, IDemonymn>()
            .As<DemonymnInfo>();

        CreateMap<CountryNameTranslation, INativeNameInfo>()
            .As<NativeNameInfo>();

        CreateMap<CountryNames, ICountryNames>()
            .As<CountryNamesInfo>();

        CreateMap<CountryNativeName, INativeNameInfo>()
            .As<NativeNameInfo>();

        CreateMap<CoatOfArms, ICoatOfArms>()
            .As<CoatOfArmsInfo>();

        CreateMap<GeoCoordinates, IGeoCoordinates>()
            .As<GeoCoordinatesInfo>();

        CreateMap<CountryMaps, IMaps>()
            .As<MapsInfo>();

        CreateMap<CountryDialing, IDialingInfo>()
            .As<DialingInfo>();

        CreateMap<CountryFlag, IFlags>()
            .As<FlagsInfo>();

        CreateMap<PostalCode, IPostalCodeInfo>()
            .As<PostalCodeInfo>();

        CreateMap<CountryCapital, ICapitalInfo>()
            .As<CapitalInfo>();

        CreateMap<CountryVehicle, IVehicleInfo>()
            .As<VehicleInfo>();

        CreateMap<Country, CountryInfo>();
        CreateMap<Demonymn, DemonymnInfo>().ForMember(x => x.Language, x => x.MapFrom(t => t.Language.Iso_639_3));
        CreateMap<CountryNameTranslation, NativeNameInfo>().ForMember(x => x.Language, x => x.MapFrom(t => t.Language.Iso_639_1));
        CreateMap<CountryNames, CountryNamesInfo>();
        CreateMap<CountryNativeName, NativeNameInfo>().ForMember(x => x.Language, x => x.MapFrom(t => t.Language.Iso_639_3));
        CreateMap<CoatOfArms, CoatOfArmsInfo>();
        CreateMap<GeoCoordinates, GeoCoordinatesInfo>();
        CreateMap<CountryMaps, MapsInfo>();
        CreateMap<CountryDialing, DialingInfo>()
            .ForMember(x => x.Suffixes, x => x.MapFrom(t => t.Suffixes.Split(",", StringSplitOptions.TrimEntries).ToList()));

        CreateMap<CountryFlag, FlagsInfo>();
        CreateMap<PostalCode, PostalCodeInfo>();
        CreateMap<CountryCapital, CapitalInfo>();
        CreateMap<CountryVehicle, VehicleInfo>()
            .ForMember(
                x => x.InternationalRegistrationCodes,
                x => x.MapFrom(t => t.InternationalRegistrationCodes.Split(",", StringSplitOptions.TrimEntries))
             );

        CreateMap<IReadOnlyList<GiniCoefficient>, IReadOnlyDictionary<int, decimal>>()
            .ConstructUsing(x => new Dictionary<int, decimal>(x.Select(x => new KeyValuePair<int, decimal>(x.Year, x.Value))));

        CreateProjection<Country, CountryInfo>()
            .ForMember(x => x.Id, x => x.MapFrom(t => t.Code))
            .ForMember(x => x.NameTranslations, x => x.MapFrom(t => t.NameTranslations))
            .ForMember(x => x.BorderingCountries, x => x.MapFrom(t => t.BorderingCountries.Select(x => x.Code).ToList()))
            .ForMember(x => x.TopLevelDomains, x => x.MapFrom(t => t.TopLevelDomains.Select(x => x.Name).ToList()))
            .ForMember(x => x.Languages, x => x.MapFrom(t => t.Languages.Select(x => x.Iso_639_3).ToList()))
            .ForMember(x => x.Currencies, x => x.MapFrom(t => t.Currencies.Select(x => x.IsoCode).ToList()))
            .ForMember(x => x.AlternateSpellings, x => x.MapFrom(t => t.AlternateSpellings.Select(x => x.Name).ToList()))
            .ForMember(x => x.Continents, x => x.MapFrom(t => t.Continents.Select(x => x.Name).ToList()))
            .ForMember(x => x.DialingInfo, x => x.MapFrom(t => t.Dialing))
            .ForMember(x => x.Flags, x => x.MapFrom(t => t.Flag))
            .ForMember(x => x.VehicleInfo, x => x.MapFrom(t => t.Vehicle))
            .ForMember(x => x.PostalCodeInfo, x => x.MapFrom(t => t.PostalCode))
            .ForMember(x => x.Capitals, x => x.MapFrom(t => t.Capitals.Select(x => x.Name).ToList()))
            .ForMember(x => x.CapitalInfo, x => x.MapFrom(t => t.Capital));
    }

    private void MapCountryInfoToCountry()
    {
        CreateMap<ICountryInfo, Country>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.BorderingCountries, x => x.Ignore())
            .ForMember(x => x.Dialing, x => x.MapFrom(t => t.DialingInfo))
            .ForMember(x => x.Flag, x => x.MapFrom(t => t.Flags))
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

        CreateMap<string, TopLevelDomain>()
            .ForMember(x => x.Name, x => x.MapFrom(t => t));

        CreateMap<string, AlternateSpelling>()
            .ForMember(x => x.Name, x => x.MapFrom(t => t));

        CreateMap<string, Continent>()
            .ForMember(x => x.Name, x => x.MapFrom(t => t));

        CreateMap<INativeNameInfo, CountryNameTranslation>()
            .ConvertUsing<CountryNameTranslationSingleMapping>();

        CreateMap<IDemonymn, Demonymn>()
            .ConvertUsing<DemonymnSingleMapping>();

        CreateMap<ICoatOfArms, CoatOfArms>();
        CreateMap<IGeoCoordinates, GeoCoordinates>();
        CreateMap<IMaps, CountryMaps>();
        CreateMap<IFlags, CountryFlag>();
        CreateMap<IVehicleInfo, CountryVehicle>()
            .ForMember(x => x.InternationalRegistrationCodes, x => x.MapFrom(t => string.Join(",", t.InternationalRegistrationCodes)));

        CreateMap<IPostalCodeInfo, PostalCode>();
        CreateMap<IDialingInfo, CountryDialing>()
            .ForMember(x => x.Suffixes, x => x.MapFrom(t => string.Join(",", t.Suffixes)));

        CreateMap<INativeNameInfo, CountryNativeName>()
            .ConvertUsing<CountryNativeNameSingleMapping>();

        CreateMap<ICountryNames, CountryNames>()
            .AfterMap((s, d) => d.NativeNames = d.NativeNames.Where(f => f != null).ToList());

        CreateMap<IReadOnlyList<string>, IReadOnlyList<Language>>()
            .ConvertUsing<LanguageMappingResolver>();

        CreateMap<IReadOnlyList<string>, IReadOnlyList<Currency>>()
            .ConvertUsing<CurrencyMappingResolver>();

        CreateMap<IReadOnlyList<string>, IReadOnlyList<TopLevelDomain>>()
            .ConvertUsing<TopLevelDomainMappingResolver>();

        CreateMap<IReadOnlyList<string>, IReadOnlyList<AlternateSpelling>>()
            .ConvertUsing<AlternateSpellingMappingResolver>();

        CreateMap<IReadOnlyList<string>, IReadOnlyList<Continent>>()
            .ConvertUsing<ContinentMappingResolver>();

        CreateMap<IReadOnlyList<IDemonymn>, IReadOnlyList<Demonymn>>()
            .ConvertUsing<DemonymnMappingResolver>();

        CreateMap<IReadOnlyList<INativeNameInfo>, IReadOnlyList<CountryNameTranslation>>()
            .ConvertUsing<CountryNameTranslationMappingResolver>();

        CreateMap<IReadOnlyDictionary<int, decimal>, IReadOnlyList<GiniCoefficient>>()
            .ConstructUsing(x => x.Keys.Select(t => new GiniCoefficient
            {
                Year = t,
                Value = x[t]
            }).ToList());
    }
}
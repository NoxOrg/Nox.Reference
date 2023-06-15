using AutoMapper;

namespace Nox.Reference.Data.World.Mappings;

internal class CountryMapping : Profile
{
    public CountryMapping()
    {
        MapCountryInfoToCountry();

        MapCountryToCountryInfo();
    }

    private void MapCountryToCountryInfo()
    {
        CreateMap<Demonymn, DemonymnInfo>().ForMember(x => x.Language, x => x.MapFrom(t => t.Language.Iso_639_3));
        CreateMap<CountryNameTranslation, CountryNameTranslationInfo>().ForMember(x => x.Language, x => x.MapFrom(t => t.Language.Iso_639_1));
        CreateMap<CountryNativeName, CountryNameTranslationInfo>().ForMember(x => x.Language, x => x.MapFrom(t => t.Language.Iso_639_3));
        CreateMap<CoatOfArms, CoatOfArmsInfo>();
        CreateMap<GeoCoordinates, GeoCoordinatesInfo>();
        CreateMap<CountryMaps, MapsInfo>();
        CreateMap<CountryDialing, DialingInfo>()
            .ForMember(x => x.Suffixes, x => x.MapFrom(t => t.Suffixes.Split(",", StringSplitOptions.TrimEntries).ToList()));
        CreateMap<CountryNames, CountryNamesInfo>()
            .ForMember(x => x.NativeNames, x => x.Ignore())
            .ForMember(x => x.NativeNamesDictionary, x => x.MapFrom(t => t.NativeNames == null ? null : t.NativeNames.ToDictionary(x => x.Language.Iso_639_3, x => x)));

        CreateMap<CountryFlag, FlagsInfo>();
        CreateMap<PostalCode, PostalCodeInfo>();
        CreateMap<CountryCapital, CapitalInfo>()
            .ForMember(x => x.LatLong, x => x.MapFrom(x => new List<decimal>
            {
                (x.GeoCoordinates != null && x.GeoCoordinates.Latitude.HasValue) ? x.GeoCoordinates.Latitude.Value : 0m,
                (x.GeoCoordinates != null && x.GeoCoordinates.Longitude.HasValue) ? x.GeoCoordinates.Longitude.Value : 0m
            }));
        CreateMap<CountryVehicle, CountryVehicleInfo>()
            .ForMember(
                x => x.InternationalRegistrationCodes,
                x => x.MapFrom(t => t.InternationalRegistrationCodes.Split(",", StringSplitOptions.TrimEntries))
             );

        CreateMap<IReadOnlyList<GiniCoefficient>, IReadOnlyDictionary<int, decimal>>()
            .ConstructUsing(x => new Dictionary<int, decimal>(x.Select(x => new KeyValuePair<int, decimal>(x.Year, x.Value))));

        CreateMap<Country, CountryInfo>()
            .ForMember(x => x.Id, x => x.MapFrom(t => t.Code))
            .ForMember(x => x.BorderingCountries, x => x.MapFrom(t => t.BorderingCountries.Select(x => x.Code).ToList()))
            .ForMember(x => x.TopLevelDomains, x => x.MapFrom(t => t.TopLevelDomains.Select(x => x.Name).ToList()))
            .ForMember(x => x.Languages, x => x.MapFrom(t => t.Languages.Select(x => x.Iso_639_3).ToList()))
            .ForMember(x => x.Currencies, x => x.MapFrom(t => t.Currencies.Select(x => x.IsoCode).ToList()))
            .ForMember(x => x.AlternateSpellings, x => x.MapFrom(t => t.AlternateSpellings.Select(x => x.Name).ToList()))
            .ForMember(x => x.Continents, x => x.MapFrom(t => t.Continents.Select(x => x.Name).ToList()))
            .ForMember(x => x.DialingInfo, x => x.MapFrom(t => t.Dialing))
            .ForMember(x => x.VatNumberDefinition, x => x.MapFrom(t => t.VatNumberDefinition))
            .ForMember(x => x.Flags, x => x.MapFrom(t => t.Flag))
            .ForMember(x => x.VehicleInfo, x => x.MapFrom(t => t.Vehicle))
            .ForMember(x => x.PostalCodeInfo, x => x.MapFrom(t => t.PostalCode))
            .ForMember(x => x.Capitals, x => x.MapFrom(t => t.Capitals.Select(x => x.Name).ToList()))
            .ForMember(x => x.GiniCoefficients, x => x.Ignore())
            .ForMember(x => x.GiniCoefficientsDictionary, x => x.MapFrom(t => t.GiniCoefficients == null ? null : t.GiniCoefficients.ToDictionary(x => x.Year, x => x.Value)))
            .ForMember(x => x.TimeZones, x => x.MapFrom(x => x.TimeZones.Select(x => x.Code)))
            .ForMember(x => x.LanguagesDictionary, x => x.MapFrom(x => x.Languages.ToDictionary(x => x.Iso_639_3, x => x.Name)))
            .ForMember(x => x.DemonymsDictionary, x => x.MapFrom(x => x.Demonyms.ToDictionary(x => x.Language.Iso_639_3, x => new DemonymnInfo
            {
                Masculine = x.Masculine,
                Feminine = x.Feminine,
                Language = x.Language.Iso_639_3
            })))
            .ForMember(x => x.CurrenciesDictionary, x => x.MapFrom(x => x.Currencies.ToDictionary(x => x.IsoCode, x => new CountryCurrencyInfo
            {
                Name = x.Name,
                Symbol = x.Symbol
            })))
            .ForMember(x => x.NameTranslationsDictionary, x => x.MapFrom(t => t.NameTranslations.ToDictionary(x => x.Language.Iso_639_3, x => new CountryNameTranslationInfo
            {
                Language = x.Language.Iso_639_3,
                CommonName = x.CommonName,
                OfficialName = x.OfficialName
            })))
            .ForMember(x => x.CapitalInfo, x => x.MapFrom(t => t.Capital));
    }

    private void MapCountryInfoToCountry()
    {
        CreateMap<CountryInfo, Country>()
            .ForMember(x => x.BorderingCountries, x => x.Ignore())
            .ForMember(x => x.NameTranslations, x => x.Ignore())
            .ForMember(x => x.Continents, x => x.Ignore())
            .ForMember(x => x.AlternateSpellings, x => x.Ignore())
            .ForMember(x => x.Demonyms, x => x.Ignore())
            .ForMember(x => x.TopLevelDomains, x => x.Ignore())
            .ForMember(x => x.Currencies, x => x.Ignore())
            .ForMember(x => x.Languages, x => x.Ignore())
            .ForMember(x => x.TimeZones, x => x.Ignore())
            .ForMember(x => x.Dialing, x => x.MapFrom(t => t.DialingInfo))
            .ForMember(x => x.VatNumberDefinition, x => x.MapFrom(t => t.VatNumberDefinition))
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

        CreateMap<CountryNameTranslationInfo, CountryNameTranslation>()
            .ConvertUsing<CountryNameTranslationSingleMapping>();
        CreateMap<CountryNameTranslation, CountryNameTranslationInfo>();

        CreateMap<DemonymnInfo, Demonymn>()
            .ConvertUsing<DemonymnSingleMapping>();
        CreateMap<Demonymn, DemonymnInfo>();

        CreateMap<CoatOfArmsInfo, CoatOfArms>();
        CreateMap<GeoCoordinatesInfo, GeoCoordinates>();
        CreateMap<MapsInfo, CountryMaps>();
        CreateMap<FlagsInfo, CountryFlag>();
        CreateMap<CountryVehicleInfo, CountryVehicle>()
            .ForMember(x => x.InternationalRegistrationCodes, x => x.MapFrom(t => string.Join(",", t.InternationalRegistrationCodes)));

        CreateMap<PostalCodeInfo, PostalCode>();
        CreateMap<DialingInfo, CountryDialing>()
            .ForMember(x => x.Suffixes, x => x.MapFrom(t => string.Join(",", t.Suffixes)));

        CreateMap<CountryNameTranslationInfo, CountryNativeName>()
            .ConvertUsing<CountryNativeNameSingleMapping>();

        CreateMap<CountryNamesInfo, CountryNames>()
            .AfterMap((s, d) => d.NativeNames = d.NativeNames.Where(f => f != null).ToList());

        CreateMap<IReadOnlyDictionary<int, decimal>, IReadOnlyList<GiniCoefficient>>()
            .ConstructUsing(x => x.Keys.Select(t => new GiniCoefficient
            {
                Year = t,
                Value = x[t]
            }).ToList());
    }
}
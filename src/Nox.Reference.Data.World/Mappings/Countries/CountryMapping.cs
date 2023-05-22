using AutoMapper;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Mappings;

internal class CountryMapping : Profile
{
    public CountryMapping()
    {
        //Write
        MapCountryInfoToCountry();
    }

    private void MapCountryInfoToCountry()
    {
        CreateMap<CountryInfo, Country>()
            .ForMember(x => x.Id, x => x.Ignore())
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

        CreateMap<NativeNameInfo, CountryNameTranslation>()
            .ConvertUsing<CountryNameTranslationSingleMapping>();

        CreateMap<DemonymnInfo, Demonymn>()
            .ConvertUsing<DemonymnSingleMapping>();

        CreateMap<CoatOfArmsInfo, CoatOfArms>();
        CreateMap<GeoCoordinatesInfo, GeoCoordinates>();
        CreateMap<MapsInfo, CountryMaps>();
        CreateMap<FlagsInfo, CountryFlag>();
        CreateMap<VehicleInfo, CountryVehicle>()
            .ForMember(x => x.InternationalRegistrationCodes, x => x.MapFrom(t => string.Join(",", t.InternationalRegistrationCodes)));

        CreateMap<PostalCodeInfo, PostalCode>();
        CreateMap<DialingInfo, CountryDialing>()
            .ForMember(x => x.Suffixes, x => x.MapFrom(t => string.Join(",", t.Suffixes)));

        CreateMap<NativeNameInfo, CountryNativeName>()
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
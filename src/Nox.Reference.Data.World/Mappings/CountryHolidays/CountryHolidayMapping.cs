using AutoMapper;
using Nox.Reference.Data.World.Mappings.Countries;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Mappings.CountryHolidays
{
    internal class CountryHolidayMapping : Profile
    {
        public CountryHolidayMapping()
        {
            //In
            CreateMap<CountryHolidayInfo, CountryHoliday>();

            CreateMap<string, Country>().ConvertUsing<CountrySingleMapping>();
            CreateMap<HolidayDataInfo, HolidayData>().ReverseMap();
            CreateMap<StateHolidayInfo, StateHoliday>().ReverseMap();
            CreateMap<LocalHolidayNameInfo, LocalHolidayName>().ReverseMap();
            CreateMap<RegionHolidayInfo, RegionHoliday>().ReverseMap();

            CreateMap<CountryHoliday, CountryHolidayInfo>()
                .ForMember(x => x.Country, x => x.MapFrom(t => t.Country.AlphaCode2));
        }
    }
}
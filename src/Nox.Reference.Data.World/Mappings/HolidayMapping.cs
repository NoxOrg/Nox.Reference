using AutoMapper;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Mappings
{
    internal class HolidayMapping : Profile
    {
        public HolidayMapping()
        {
            //In
            CreateMap<CountryHolidayInfo, CountryHoliday>().ReverseMap();
            CreateMap<HolidayDataInfo, HolidayData>().ReverseMap();
            CreateMap<StateHolidayInfo, StateHoliday>().ReverseMap();
            CreateMap<LocalHolidayNameInfo, LocalHolidayName>().ReverseMap();
            CreateMap<RegionHolidayInfo, RegionHoliday>().ReverseMap();

            //Out
            CreateProjection<CountryHoliday, CountryHolidayInfo>();

            CreateMap<CountryHoliday, ICountryHolidayInfo>().As<CountryHolidayInfo>();
            CreateMap<HolidayData, IHolidayData>().As<HolidayDataInfo>();
            CreateMap<StateHoliday, IStateHolidayInfo>().As<StateHolidayInfo>();
            CreateMap<LocalHolidayName, ILocalHolidayName>().As<LocalHolidayNameInfo>();
            CreateMap<RegionHoliday, IRegionHolidayInfo>().As<RegionHolidayInfo>();
        }
    }
}
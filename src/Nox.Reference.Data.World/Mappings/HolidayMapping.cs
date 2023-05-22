using AutoMapper;
using Nox.Reference.Data.World.Models;

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
        }
    }
}
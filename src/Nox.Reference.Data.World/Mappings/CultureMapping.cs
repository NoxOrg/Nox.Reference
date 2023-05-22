using AutoMapper;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Mappings
{
    internal class CultureMapping : Profile
    {
        public CultureMapping()
        {
            CreateMap<CultureInfo, Culture>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.Name, x => x.MapFrom(x => x.Id))
                .ReverseMap();
            CreateMap<DateFormatInfo, DateFormat>();
            CreateMap<NumberFormatInfo, NumberFormat>();
        }
    }
}
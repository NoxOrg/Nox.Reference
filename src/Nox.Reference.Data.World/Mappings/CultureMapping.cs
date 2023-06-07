using AutoMapper;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Mappings
{
    internal class CultureMapping : Profile
    {
        public CultureMapping()
        {
            CreateMap<CultureInfo, Culture>()
                .ForMember(x => x.Name, x => x.MapFrom(x => x.Id))
                .ForMember(x => x.Country, x => x.Ignore());
            CreateMap<Culture, CultureInfo>()
                .ForMember(x => x.Id, x => x.MapFrom(x => x.Name))
                .ForMember(x => x.Country, x => x.MapFrom(t => t.Country == null ? null : t.Country.Code));

            CreateMap<DateFormatInfo, DateFormat>()
                .ReverseMap();
            CreateMap<NumberFormatInfo, NumberFormat>()
                .ReverseMap();
        }
    }
}
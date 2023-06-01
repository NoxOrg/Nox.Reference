using AutoMapper;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Mappings
{
    internal class CultureMapping : Profile
    {
        public CultureMapping()
        {
            CreateMap<CultureInfo, Culture>()
                .ForMember(x => x.EntityId, x => x.Ignore())
                .ForMember(x => x.Name, x => x.MapFrom(x => x.Id));
            CreateMap<Culture, CultureInfo>()
                .ForMember(x => x.Id, x => x.MapFrom(x => x.Name));

            CreateMap<DateFormatInfo, DateFormat>()
                .ReverseMap();
            CreateMap<NumberFormatInfo, NumberFormat>()
                .ReverseMap();
        }
    }
}
﻿using AutoMapper;
using Nox.Reference.Abstractions.Cultures;
using Nox.Reference.Data.World.Entities.Cultures;
using Nox.Reference.Data.World.Models.Cultures;

namespace Nox.Reference.Data.World.Mappings
{
    internal class CultureMapping : Profile
    {
        public CultureMapping()
        {
            CreateMap<ICultureInfo, Culture>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.Name, x => x.MapFrom(x => x.Id))
                .ReverseMap();
            CreateMap<IDateFormatInfo, DateFormat>().ReverseMap().As<DateFormatInfo>();
            CreateMap<INumberFormatInfo, NumberFormat>().ReverseMap().As<NumberFormatInfo>();

            CreateProjection<Culture, CultureInfo>()
                .ForMember(x => x.Id, x => x.MapFrom(x => x.Name));
            CreateProjection<DateFormat, DateFormatInfo>();
            CreateProjection<NumberFormat, NumberFormatInfo>();
        }
    }
}
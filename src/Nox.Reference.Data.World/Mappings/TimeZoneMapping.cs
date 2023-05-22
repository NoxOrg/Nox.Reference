using AutoMapper;
using TimeZoneInfo = Nox.Reference.Data.World.Models.TimeZoneInfo;

namespace Nox.Reference.Data.World.Mappings;

internal class TimeZoneMapping : Profile
{
    public TimeZoneMapping()
    {
#pragma warning disable S3358 // Ternary operators should not be nested
        CreateMap<TimeZoneInfo, TimeZone>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.Code, x => x.MapFrom(x => x.Id))
            .ForMember(x => x.Latitude, y => y.MapFrom(s => s.GeoCoordinates == null ? null : (s.GeoCoordinates.Latitude == null ? null : s.GeoCoordinates.Latitude)))
            .ForMember(x => x.Longitude, y => y.MapFrom(s => s.GeoCoordinates == null ? null : (s.GeoCoordinates.Longitude == null ? null : s.GeoCoordinates.Longitude)))
            .ReverseMap();
#pragma warning restore S3358 // Ternary operators should not be nested
    }
}
using AutoMapper;
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses.DataContext;

internal class MacAddressMapping : Profile
{
    public MacAddressMapping()
    {
        CreateMap<IMacAddressInfo, MacAddress>()
            .ForMember(x => x.Id, x => x.Ignore());

        CreateProjection<MacAddress, MacAddressInfo>()
            .ForMember(x => x.Id, x => x.MapFrom(t => t.MacPrefix));

        CreateMap<MacAddress, IMacAddressInfo>()
            .As<MacAddressInfo>();
    }
}
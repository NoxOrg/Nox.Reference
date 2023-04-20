using AutoMapper;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.Machine;

internal class MacAddressMapping : Profile
{
    public MacAddressMapping()
    {
        CreateMap<IMacAddressInfo, MacAddress>()
            .ForMember(x => x.Id, x => x.Ignore());

        CreateProjection<MacAddress, MacAddressInfo>()
            .ForMember(x => x.Id, x => x.MapFrom(t => t.MacPrefix));
    }
}
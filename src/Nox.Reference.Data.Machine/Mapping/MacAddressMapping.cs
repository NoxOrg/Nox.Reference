using AutoMapper;

namespace Nox.Reference.Data.Machine;

internal class MacAddressMapping : Profile
{
    public MacAddressMapping()
    {
        CreateMap<MacAddressInfo, MacAddress>()
            .ForMember(x => x.EntityId, x => x.Ignore())
            .ReverseMap();
    }
}
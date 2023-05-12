using AutoMapper;

namespace Nox.Reference.Data.Machine;

internal class MacAddressMapping : Profile
{
    public MacAddressMapping()
    {
        CreateMap<MacAddressInfo, MacAddress>()
            .ForMember(x => x.Id, x => x.Ignore());
    }
}
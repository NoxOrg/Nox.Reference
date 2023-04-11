using AutoMapper;
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.Data.Mappings
{
    internal class MacAddressMapping : Profile
    {
        public MacAddressMapping()
        {
            CreateMap<IMacAddressInfo, MacAddress>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ReverseMap();
        }
    }
}
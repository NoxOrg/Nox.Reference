using AutoMapper;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data.Models;

namespace Nox.Reference.Data.Mappings
{
    internal class MacAddressMapping : Profile
    {
        public MacAddressMapping()
        {
            CreateMap<IMacAddressInfo, MacAddress>()
                .ForMember(x => x.Id, x => x.Ignore());

            CreateMap<MacAddress, MacAddressInfo>()
                .ForMember(x => x.Id, x => x.MapFrom(t => t.MacPrefix));
            CreateMap<MacAddress, IMacAddressInfo>()
                .As<MacAddressInfo>();
        }
    }
}
using AutoMapper;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Entity;

namespace Nox.Reference.GetData.Mappings
{
    public class NoxReferenceAutomapperProfile : Profile
    {
        public NoxReferenceAutomapperProfile()
        {
            CreateMap<MacAddress, IMacAddressInfo>()
                .ReverseMap();
        }
    }
}
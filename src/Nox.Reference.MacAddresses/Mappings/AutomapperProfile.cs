using AutoMapper;
using Nox.Reference.Entity;
using Nox.Reference.MacAddresses.Models;

namespace Nox.Reference.MacAddresses;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<MacAddress, MacAddressInfo>();
    }
}
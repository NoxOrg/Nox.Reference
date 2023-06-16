using System.Numerics;
using AutoMapper;
using Nox.Reference.Data.IpAddress.Models;

namespace Nox.Reference.Data.IpAddress;

internal class IpAddressMapping : Profile
{
    public IpAddressMapping()
    {
        CreateMap<string, IpAddressChunk>().ConstructUsing((x, ctx) => IpAddressChunk.CreateIpAddressChunkFromNumber(BigInteger.Parse(x)));

        CreateMap<IpAddressChunk, string>().ConstructUsing(x => x.ToString());

        CreateMap<IpAddressInfo, IpAddress>()
            .ForMember(x => x.StartAddress, x => x.MapFrom(t => t.StartAddress))
            .ForMember(x => x.EndAddress, x => x.MapFrom(t => t.EndAddress))
            .ReverseMap();
    }
}
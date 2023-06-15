using System.Numerics;
using AutoMapper;
using Nox.Reference.Data.IpAddress.Models;

namespace Nox.Reference.Data.IpAddress;

internal class IpAddressMapping : Profile
{
    public IpAddressMapping()
    {
        CreateMap<string, IpAddressChunk>().ConstructUsing((x, ctx) =>
        {
            var uint128 = BigInteger.Parse(x);
            ulong startPart = (ulong)(uint128 >> 64);
            ulong endPart = (ulong)(uint128 & ulong.MaxValue);

            return new IpAddressChunk(startPart, endPart);
        });

        CreateMap<IpAddressChunk, string>().ConstructUsing(x => x.ToString());

        CreateMap<IpAddressInfo, IpAddress>()
            .ForMember(x => x.StartAddress, x => x.MapFrom(t => t.StartAddress))
            //.ForMember(x => x.EndAddress, x => x.MapFrom(t => t.EndAddress))
            .ReverseMap();
    }
}
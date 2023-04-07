using AutoMapper;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data.Entities;

namespace Nox.Reference.Data.Seeds;

internal class MacAddressDataSeed : NoxReferenceDatabaseSeedBase<IMacAddressInfo, MacAddress>
{
    public MacAddressDataSeed(NoxReferenceDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}
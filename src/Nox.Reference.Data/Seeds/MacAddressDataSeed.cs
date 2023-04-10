using AutoMapper;
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.Data.Seeds;

internal class MacAddressDataSeed : NoxReferenceDatabaseSeedBase<IMacAddressInfo, MacAddress>
{
    public MacAddressDataSeed(NoxReferenceDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}
using AutoMapper;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data.Entities;

namespace Nox.Reference.Data.Repositories;

internal class MacAddressRepository : NoxReferenceRepositoryBase<MacAddress, IMacAddressInfo>
{
    public MacAddressRepository(NoxReferenceDbContext dataContext, IMapper mapper) : base(dataContext, mapper)
    {
    }
}
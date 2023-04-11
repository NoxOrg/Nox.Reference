using AutoMapper;
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.Data.Repositories;

internal class MacAddressRepository : INoxReferenceKeyRepository<IMacAddressInfo>
{
    private readonly NoxReferenceDbContext _dataContext;
    private readonly IMapper _mapper;

    public MacAddressRepository(
        NoxReferenceDbContext dataContext,
        IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public IMacAddressInfo? Get(string key)
    {
        var entity = _dataContext
            .Set<MacAddress>()
            .FirstOrDefault(x => x.MacPrefix == key);

        if (entity == null)
        {
            return null;
        }

        var info = _mapper.Map<IMacAddressInfo>(entity);

        return info;
    }
}
using AutoMapper;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data;
using Nox.Reference.Entity;
using Nox.Reference.MacAddresses.Helpers;
using Nox.Reference.MacAddresses.Models;

namespace Nox.Reference.MacAddresses;

internal partial class MacAddressService : IMacAddressService
{
    private readonly NoxReferenceRepository<MacAddress> _repository;
    private readonly IMapper _mapper;

    public MacAddressService(
        NoxReferenceRepository<MacAddress> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IMacAddressInfo? GetMacAddressInfo(string id)
    {
        var macAddressPrefix = MacAddressHelper.GetMacAddressPrefix(id);

        var entity = _repository.Get(macAddressPrefix);
        var info = _mapper.Map<MacAddressInfo>(entity);

        return info;
    }
}
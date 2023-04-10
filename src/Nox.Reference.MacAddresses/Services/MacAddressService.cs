using AutoMapper;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data.Repositories;
using Nox.Reference.MacAddresses.Helpers;

namespace Nox.Reference.MacAddresses;

internal class MacAddressService : IMacAddressService
{
    private readonly INoxReferenceRepository<IMacAddressInfo> _repository;

    public MacAddressService(INoxReferenceRepository<IMacAddressInfo> repository)
    {
        _repository = repository;
    }

    public IMacAddressInfo? GetMacAddressInfo(string id)
    {
        var macAddressPrefix = MacAddressHelper.GetMacAddressPrefix(id);

        var info = _repository.Get(0);

        return info;
    }
}
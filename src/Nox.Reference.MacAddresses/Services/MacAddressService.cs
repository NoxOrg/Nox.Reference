using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data.Repositories;
using Nox.Reference.MacAddresses.Helpers;

namespace Nox.Reference.MacAddresses;

internal class MacAddressService : IMacAddressService
{
    private readonly INoxReferenceKeyRepository<IMacAddressInfo> _repository;

    public MacAddressService(INoxReferenceKeyRepository<IMacAddressInfo> repository)
    {
        _repository = repository;
    }

    public IMacAddressInfo? GetMacAddressInfo(string id)
    {
        var macAddressPrefix = MacAddressHelper.GetMacAddressPrefix(id);

        var info = _repository.Get(macAddressPrefix);

        return info;
    }
}
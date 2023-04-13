using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data.Repositories;
using Nox.Reference.MacAddresses.Helpers;

namespace Nox.Reference.MacAddresses;

internal class MacAddressService : IMacAddressService
{
    private readonly INoxReferenceContext<IMacAddressInfo> _repository;

    public MacAddressService(INoxReferenceContext<IMacAddressInfo> repository)
    {
        _repository = repository;
    }

    public IMacAddressInfo? GetMacAddressInfo(string id)
    {
        var macAddressPrefix = MacAddressHelper.GetMacAddressPrefix(id);
        var tt = _repository.Set.Take(10).ToList();
        var info = _repository.Set.FirstOrDefault(x => x.MacPrefix == macAddressPrefix);

        return info;
    }
}
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data.Repositories;
using Nox.Reference.MacAddresses.Helpers;

namespace Nox.Reference.MacAddresses;

internal class MacAddressService : IMacAddressService
{
    private readonly INoxReferenceContext<IMacAddressInfo> _context;

    public MacAddressService(INoxReferenceContext<IMacAddressInfo> repository)
    {
        _context = repository;
    }

    public IMacAddressInfo? GetMacAddressInfo(string id)
    {
        var macAddressPrefix = MacAddressHelper.GetMacAddressPrefix(id);
        var info = _context.Query().FirstOrDefault(x => x.MacPrefix == macAddressPrefix);

        return info;
    }
}
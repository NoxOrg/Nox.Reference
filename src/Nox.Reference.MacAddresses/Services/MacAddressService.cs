using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Common;

namespace Nox.Reference.MacAddresses;

internal class MacAddressService : IMacAddressService
{
    private static IReadOnlyList<IMacAddressInfo> _macAddresses = new List<IMacAddressInfo>();
    private readonly LookupHandler<IMacAddressInfo> _lookupHandler;

    public MacAddressService(LookupHandler<IMacAddressInfo> lookupHandler)
    {
        _lookupHandler = lookupHandler;
    }

    public static void Init(IEnumerable<IMacAddressInfo> macAddresses)
    {
        _macAddresses = new List<IMacAddressInfo>(macAddresses);
    }

    public IEnumerable<IMacAddressInfo> LookupMacAddressInfoByOrganiztion(string searchKey)
    {
        return _lookupHandler.Lookup(_macAddresses, x => x.OrganizationName.Contains(searchKey, StringComparison.OrdinalIgnoreCase));
    }

    public IReadOnlyList<IMacAddressInfo> GetMacAddresses()
    {
        return _macAddresses;
    }

    public IMacAddressInfo? GetMacAddressInfo(string organizationName)
    {
        return _macAddresses.FirstOrDefault(x => x.OrganizationName.Equals(organizationName, StringComparison.OrdinalIgnoreCase));
    }
}
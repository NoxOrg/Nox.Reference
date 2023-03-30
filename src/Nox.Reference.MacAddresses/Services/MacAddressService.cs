using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses;

internal class MacAddressService : IMacAddressService
{
    private readonly IReadOnlyList<IMacAddressInfo> _macAddresses;

    public MacAddressService(
        IEnumerable<IMacAddressInfo> macAddresses)
    {
        _macAddresses = new List<IMacAddressInfo>(macAddresses);
    }

    public IEnumerable<IMacAddressInfo> LookupMacAddressInfoByOrganiztion(string searchKey)
    {
        return _macAddresses.Where(x => x.OrganizationName.Contains(searchKey, StringComparison.OrdinalIgnoreCase));
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
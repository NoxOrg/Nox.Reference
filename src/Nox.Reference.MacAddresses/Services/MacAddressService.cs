using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses;

internal class MacAddressService : IMacAddressService
{
    private readonly IReadOnlyList<IMacAddressInfo> _macAddresses;

    public MacAddressService(IEnumerable<IMacAddressInfo> macAddresses)
    {
        _macAddresses = new List<IMacAddressInfo>(macAddresses);
    }

    public IEnumerable<IMacAddressInfo> FindMacAddressByVendor(string searchKey)
    {
        return _macAddresses.Where(x => x.Vendor.Contains(searchKey, StringComparison.OrdinalIgnoreCase));
    }

    public IReadOnlyList<IMacAddressInfo> GetMacAddresses()
    {
        return _macAddresses;
    }

    public IMacAddressInfo? GetVendorMacAddress(string vendorName)
    {
        return _macAddresses.FirstOrDefault(x => x.Vendor.Equals(vendorName, StringComparison.OrdinalIgnoreCase));
    }
}
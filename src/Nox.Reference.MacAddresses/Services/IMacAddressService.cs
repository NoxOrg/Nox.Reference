using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses
{
    public interface IMacAddressService
    {
        IReadOnlySet<IMacAddressInfo> GetMacAddresses();

        IMacAddressInfo GetVendorMacAddress(string vendorName);

        IEnumerable<IMacAddressInfo> FindMacAddressByVendor(string pattern);
    }
}
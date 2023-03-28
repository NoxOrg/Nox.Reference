using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses
{
    public interface IMacAddressService
    {
        /// <summary>
        /// Get all mac addresses with vendors.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<IMacAddressInfo> GetMacAddresses();

        /// <summary>
        /// Get mac address info for particular vendor.
        /// </summary>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        IMacAddressInfo? GetVendorMacAddress(string vendorName);

        /// <summary>
        /// Search Mac address by vendor matches.
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IEnumerable<IMacAddressInfo> FindMacAddressByVendor(string searchKey);
    }
}
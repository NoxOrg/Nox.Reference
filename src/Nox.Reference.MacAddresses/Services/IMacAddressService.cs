using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses
{
    public interface IMacAddressService
    {
        /// <summary>
        /// Get all Mac Addresses info.
        /// </summary>
        /// <returns>Collection of IMacAddressInfo</returns>
        IReadOnlyList<IMacAddressInfo> GetMacAddresses();

        /// <summary>
        /// Get mac address info for particular vendor.
        /// </summary>
        /// <param name="organizationName">Vendor name</param>
        /// <returns>IMacAddressInfo</returns>
        IMacAddressInfo? GetMacAddressInfo(string organizationName);

        /// <summary>
        /// Search Mac Address by matches in vendor name.
        /// </summary>
        /// <param name="searchKey">Find any occurrences in a vendor name.</param>
        /// <returns>Collection of IMacAddressInfo</returns>
        IEnumerable<IMacAddressInfo> LookupMacAddressInfoByOrganiztion(string searchKey);
    }
}
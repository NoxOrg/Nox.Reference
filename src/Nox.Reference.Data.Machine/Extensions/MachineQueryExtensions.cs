namespace Nox.Reference.Data.Machine;

public static class MachineQueryExtensions
{
    /// <summary>
    /// Get mac address info by mac prefix
    /// <example>
    /// <code>
    /// MacAddresses.Get("00:16:F6:11:22:33")
    /// MacAddresses.Get("00 16 F6 11 22 33")
    /// MacAddresses.Get("00-16-F6-11-22-33")
    /// MacAddresses.Get("0016F6112233")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">IQueryable incoming query</param>
    /// <param name="macAddress">Mac address</param>
    /// <returns>Mac address entity or null</returns>
    public static MacAddress? Get(this IQueryable<MacAddress> query, string macAddress)
    {
        return query.GetByMacAddress(macAddress);
    }

    /// <summary>
    /// Get mac address info by mac prefix
    /// <example>
    /// <code>
    /// MacAddresses.GetByMacAddress("00:16:F6:11:22:33")
    /// MacAddresses.GetByMacAddress("00 16 F6 11 22 33")
    /// MacAddresses.GetByMacAddress("00-16-F6-11-22-33")
    /// MacAddresses.GetByMacAddress("0016F6112233")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">IQueryable incoming query</param>
    /// <param name="macAddress">Mac address</param>
    /// <returns>Mac address entity or null</returns>
    public static MacAddress? GetByMacAddress(this IQueryable<MacAddress> query, string macAddress)
    {
        var macPrefix = MacAddressHelper.GetMacAddressPrefix(macAddress);
        return query.FirstOrDefault(x => x.MacPrefix == macPrefix);
    }
}
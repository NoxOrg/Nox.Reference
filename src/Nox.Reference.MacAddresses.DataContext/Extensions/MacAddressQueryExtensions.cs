using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddress.DataContext;

public static class MacAddressQueryExtensions
{
    public static IMacAddressInfo? Get(this IQueryable<IMacAddressInfo> query, string macAddress)
    {
        var macPrefix = MacAddressHelper.GetMacAddressPrefix(macAddress);
        return query.FirstOrDefault(x => x.Id == macPrefix);
    }
}
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.Machine;

public static class MachineQueryExtensions
{
    public static IMacAddressInfo? Get(this IQueryable<IMacAddressInfo> query, string macAddress)
    {
        var macPrefix = MacAddressHelper.GetMacAddressPrefix(macAddress);
        return query.FirstOrDefault(x => x.Id == macPrefix);
    }
}
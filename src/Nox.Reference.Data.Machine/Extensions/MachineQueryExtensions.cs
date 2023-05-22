namespace Nox.Reference.Data.Machine;

public static class MachineQueryExtensions
{
    public static MacAddress? Get(this IQueryable<MacAddress> query, string macAddress)
    {
        var macPrefix = MacAddressHelper.GetMacAddressPrefix(macAddress);
        return query.FirstOrDefault(x => x.MacPrefix == macPrefix);
    }
}
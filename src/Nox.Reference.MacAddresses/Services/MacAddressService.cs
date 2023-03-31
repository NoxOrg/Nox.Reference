using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.MacAddresses.Helpers;

namespace Nox.Reference.MacAddresses;

internal class MacAddressService : IMacAddressService
{
    private static IReadOnlyList<IMacAddressInfo> _macAddresses = new List<IMacAddressInfo>();

    public static void Init(IEnumerable<IMacAddressInfo> macAddresses)
    {
        _macAddresses = new List<IMacAddressInfo>(macAddresses);
    }

    public IMacAddressInfo? GetMacAddressInfo(string id)
    {
        var macAddressPrefix = MacAddressHelper.GetMacAddressPrefix(id);
        return _macAddresses.FirstOrDefault(x => x.Id.Equals(macAddressPrefix, StringComparison.OrdinalIgnoreCase));
    }
}
using Newtonsoft.Json;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.MacAddresses.Models;
using System.Reflection;

namespace Nox.Reference.MacAddresses;

public class MacAddressService : IMacAddressService
{
    private readonly IReadOnlyList<IMacAddressInfo> _macAddresses = new List<IMacAddressInfo>();

    public MacAddressService()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Nox.Reference.MacAddresses.json";
        if (assembly == null)
        {
            return;
        }

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return;
        }

        using var reader = new StreamReader(stream);
        var addressInfos = JsonConvert.DeserializeObject<IReadOnlyList<MacAddressInfo>>(reader.ReadToEnd());

        _macAddresses = addressInfos != null ? addressInfos : new List<IMacAddressInfo>();
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
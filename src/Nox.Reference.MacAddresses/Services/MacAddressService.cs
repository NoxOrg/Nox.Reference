using Newtonsoft.Json;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.MacAddresses.Models;
using System.Reflection;

namespace Nox.Reference.MacAddresses;

public class MacAddressService : IMacAddressService
{
    private readonly IReadOnlySet<IMacAddressInfo> _macAddresses;

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

        _macAddresses = JsonConvert.DeserializeObject<MacAddressInfo[]>(reader.ReadToEnd())
            .ToHashSet<IMacAddressInfo>();
    }

    public IEnumerable<IMacAddressInfo> FindMacAddressByVendor(string pattern)
    {
        return _macAddresses.Where(x => x.Vendor.Contains(pattern, StringComparison.OrdinalIgnoreCase));
    }

    public IReadOnlySet<IMacAddressInfo> GetMacAddresses()
    {
        return _macAddresses;
    }

    public IMacAddressInfo GetVendorMacAddress(string vendorName)
    {
        return _macAddresses.FirstOrDefault(x => x.Vendor.Equals(vendorName, StringComparison.OrdinalIgnoreCase));
    }
}
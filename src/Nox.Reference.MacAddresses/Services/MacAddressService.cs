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

        _macAddresses = (IReadOnlySet<IMacAddressInfo>)JsonConvert.DeserializeObject<HashSet<MacAddressInfo>>(reader.ReadToEnd());
    }

    public IReadOnlySet<IMacAddressInfo> GetMacAddresses() => _macAddresses;
}
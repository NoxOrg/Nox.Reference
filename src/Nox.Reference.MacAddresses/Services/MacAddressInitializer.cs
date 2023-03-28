using Newtonsoft.Json;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.MacAddresses.Models;
using System.Reflection;

namespace Nox.Reference.MacAddresses;

internal static class MacAddressInitializer
{
    private const string ResourceName = "Nox.Reference.MacAddresses.json";

    public static IEnumerable<IMacAddressInfo> GetMacAddresses()
    {
        var assembly = Assembly.GetExecutingAssembly();

        if (assembly == null)
        {
            throw new InvalidOperationException("ExecutingAssembly was not found");
        }

        using var stream = assembly.GetManifestResourceStream(ResourceName);
        if (stream == null)
        {
            throw new InvalidOperationException("Assemly stream is null or empty");
        }

        using var reader = new StreamReader(stream);
        var addressInfos = JsonConvert.DeserializeObject<IReadOnlyList<MacAddressInfo>>(reader.ReadToEnd());

        if (addressInfos == null || !addressInfos.Any())
        {
            throw new InvalidOperationException("MacAddress collection is null or empty.");
        }

        return addressInfos;
    }
}
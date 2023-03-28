using Newtonsoft.Json;
using Nox.Reference.Common;
using Nox.Reference.MacAddresses.Models;

namespace Nox.Reference.MacAddresses;

internal class MacAddressInitializer : AssemblyDataInitializer<MacAddressInfo>
{
    protected override string ResourceName => "Nox.Reference.MacAddresses.json";

    protected override IEnumerable<MacAddressInfo> CreateCollectionDataFromContent(string content)
    {
        var addressInfos = JsonConvert.DeserializeObject<MacAddressInfo[]>(content);

        if (addressInfos == null || !addressInfos.Any())
        {
            throw new InvalidOperationException("MacAddress collection is null or empty.");
        }

        return addressInfos;
    }
}
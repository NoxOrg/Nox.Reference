using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.GetData.DataSeeds;

public class MacAddressFileSeed : JsonFileDataSeedBase<IMacAddressInfo>
{
    private const string OutputFilePath = "Nox.Reference.MacAddresses.json";

    public MacAddressFileSeed(IConfiguration configuration) : base(configuration, OutputFilePath)
    {
    }
}
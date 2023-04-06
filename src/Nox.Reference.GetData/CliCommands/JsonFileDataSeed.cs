using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Common;

namespace Nox.Reference.GetData.CliCommands;

public class MacAddressFileSeed : JsonFileDataSeed<IMacAddressInfo>
{
    private const string OutputFilePath = "Nox.Reference.MacAddresses.json";

    public MacAddressFileSeed(IConfiguration configuration) : base(configuration, OutputFilePath)
    {
    }
}

public abstract class JsonFileDataSeed<TType> : INoxReferenceSeed<TType>
{
    private readonly IConfiguration _configuration;

    protected JsonFileDataSeed(IConfiguration configuration, string outputFilePath)
    {
        _configuration = configuration;
    }

    public void Seed(IEnumerable<TType> data)
    {
        var targetOutputPath = _configuration.GetValue<string>(ConfigurationConstants.TargetDataPathSettingName)!;

        var serializedOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        var jsonString = JsonSerializer.Serialize(data, serializedOptions);
        using var sw = new StreamWriter(Path.Combine(targetOutputPath, OutputFilePath));

        sw.WriteLine(jsonString);
    }
}
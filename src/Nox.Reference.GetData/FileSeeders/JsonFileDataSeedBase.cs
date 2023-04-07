using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions;

namespace Nox.Reference.GetData.DataSeeds;

public abstract class JsonFileDataSeedBase<TType> : INoxReferenceSeed<TType>
{
    private readonly IConfiguration _configuration;
    private readonly string _outputFilePath;

    protected JsonFileDataSeedBase(IConfiguration configuration, string outputFilePath)
    {
        _configuration = configuration;
        _outputFilePath = outputFilePath;
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
        using var sw = new StreamWriter(Path.Combine(targetOutputPath, _outputFilePath));

        sw.WriteLine(jsonString);
    }
}
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.GetData.DataSeeds;
using Nox.Reference.GetData.DataSeeds.MacAddresses;

namespace Nox.Reference.GetData;

internal class DataSeederExecutor : IDataSeederExecutor
{
    private readonly ILogger<DataSeederExecutor> _logger;
    private readonly IConfiguration _configuration;
    private readonly List<INoxReferenceDataSeeder> _dataSeeds = new List<INoxReferenceDataSeeder>();

    public DataSeederExecutor(
        ILogger<DataSeederExecutor> logger,
        IConfiguration configuration,
        MacAddressDataSeeder macAddressDataSeed)
    {
        _logger = logger;
        _configuration = configuration;
        _dataSeeds.Add(macAddressDataSeed);
    }

    public void Run(string? commandName = null)
    {
        _logger.LogInformation("Creating ouput folders...");
        CreateOuputFolders();
        _logger.LogInformation("Done creating ouput folders.");

        _logger.LogInformation("Start extracting data...");

        foreach (var dataSeed in _dataSeeds)
        {
            dataSeed.Execute();
        }

        _logger.LogInformation("Completed.");
    }

    private void CreateOuputFolders()
    {
        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var targetOutputPath = _configuration.GetValue<string>(ConfigurationConstants.TargetDataPathSettingName)!;

        Directory.CreateDirectory(targetOutputPath);
        _logger.LogInformation("Target path {targetOutputPath} has been created.", targetOutputPath);

        Directory.CreateDirectory(sourceOutputPath);
        _logger.LogInformation("Source path {sourceOutputPath} has been created.", sourceOutputPath);
    }
}
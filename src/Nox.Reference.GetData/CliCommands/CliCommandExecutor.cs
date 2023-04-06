using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Nox.Reference.GetData.CliCommands;

internal class CliCommandExecutor : ICliCommandExecutor
{
    private readonly ILogger<CliCommandExecutor> _logger;
    private readonly IConfiguration _configuration;

    public CliCommandExecutor(
        ILogger<CliCommandExecutor> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void Run(string? commandName = null)
    {
        _logger.LogInformation("Creating ouput folders...");
        CreateOuputFolders();
        _logger.LogInformation("Done creating ouput folders.");

        _logger.LogInformation("Start extracting data...");

        var dataSeeders = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsAssignableFrom(typeof(ICliCommandExecutor)))
            .Cast<INoxReferenceDataSeed>()
            .ToArray();

        foreach (var dataSeeder in dataSeeders)
        {
            dataSeeder.Execute();
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
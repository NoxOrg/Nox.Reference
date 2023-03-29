using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Nox.Reference.GetData.CliCommands;

public class EnviromentSetupCommand : ICliCommand
{
    private readonly ILogger<EnviromentSetupCommand> _logger;
    private readonly IConfiguration _configuration;

    public EnviromentSetupCommand(
        ILogger<EnviromentSetupCommand> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void Execute()
    {
        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var targetOutputPath = _configuration.GetValue<string>(ConfigurationConstants.TargetDataPathSettingName)!;

        Directory.CreateDirectory(targetOutputPath);
        _logger.LogInformation("Target path {targetOutputPath} has been created.", targetOutputPath);

        Directory.CreateDirectory(sourceOutputPath);
        _logger.LogInformation("Source path {sourceOutputPath} has been created.", sourceOutputPath);
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Nox.Reference.GetData.CliCommands;

public class CliCommandExecutor : ICliCommandExecutor
{
    private readonly ILogger<CliCommandExecutor> _logger;
    private readonly IConfiguration _configuration;
    private readonly CountryDataExtractCommand _countryDataExtractCommand;
    private readonly CurrencyDataExtractCommand _currencyDataExtractCommand;
    private readonly MacAddressDataExtractCommand _macAddressDataExtractCommand;

    public CliCommandExecutor(
        ILogger<CliCommandExecutor> logger,
        IConfiguration configuration,
        CountryDataExtractCommand countryDataExtractCommand,
        CurrencyDataExtractCommand currencyDataExtractCommand,
        MacAddressDataExtractCommand macAddressDataExtractCommand)
    {
        _logger = logger;
        _configuration = configuration;
        _countryDataExtractCommand = countryDataExtractCommand;
        _currencyDataExtractCommand = currencyDataExtractCommand;
        _macAddressDataExtractCommand = macAddressDataExtractCommand;
    }

    public void Run(string? commandName = null)
    {
        _logger.LogInformation("Creating ouput folders...");
        CreateOuputFolders();
        _logger.LogInformation("Done creating ouput folders.");

        _logger.LogInformation("Start extracting data...");

        _countryDataExtractCommand.Execute();
        _currencyDataExtractCommand.Execute();
        _macAddressDataExtractCommand.Execute();

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
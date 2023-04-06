using System.Globalization;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Common;

namespace Nox.Reference.GetData.CliCommands.MacAddresses;

public class MacAddressDataSeed : INoxReferenceDataSeed
{
    private const string SourceFilePath = @"MacAddresses\mac-vendor.csv";

    private readonly INoxReferenceSeed<IMacAddressInfo> _dataSeed;
    private readonly IConfiguration _configuration;
    private readonly ILogger<MacAddressDataSeed> _logger;

    public MacAddressDataSeed(
        INoxReferenceSeed<IMacAddressInfo> dataSeed,
        IConfiguration configuration,
        ILogger<MacAddressDataSeed> logger)
    {
        _dataSeed = dataSeed;
        _configuration = configuration;
        _logger = logger;
    }

    public void Execute()
    {
        _logger.LogInformation("Getting MAC Address data...");

        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;

        var sourceFilePath = Path.Combine(sourceOutputPath, SourceFilePath);

        if (!File.Exists(sourceFilePath))
        {
            DownloadSourceFileAsync()
                .Wait();
        }

        using var sr = new StreamReader(sourceFilePath);
        using var csvReader = new CsvReader(sr, CultureInfo.InvariantCulture);

        var dataRecords = new List<IMacAddressInfo>();
        while (csvReader.Read())
        {
            var data = csvReader.GetRecord<MacAddressInfo>();
            if (data == null)
            {
                throw new NoxDataExtractorException($"Unable parse data obtained from {sourceFilePath}", sourceFilePath);
            }
            dataRecords.Add(data);
        }

        _dataSeed.Seed(dataRecords);

        _logger.LogInformation("Getting MAC Address data successfuly completed...");
    }

    private async Task DownloadSourceFileAsync()
    {
        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var uriMacAddresses = _configuration.GetValue<string>(ConfigurationConstants.UriMacAddressesSettingName)!;

        var sourceFilePath = Path.Combine(sourceOutputPath, SourceFilePath);

        using var httpClient = new HttpClient();

        using var stream = await httpClient.GetStreamAsync(uriMacAddresses);
        using var fs = new FileStream(sourceFilePath, FileMode.Create, FileAccess.ReadWrite);

        await stream.CopyToAsync(fs);
    }
}
using System.Globalization;
using AutoMapper;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;

namespace Nox.Reference.Data.Machine;

internal class MachineDataSeeder : NoxReferenceDataSeederBase<MachineDbContext, MacAddressInfo, MacAddress>
{
    private const string SourceFilePath = @"MacAddresses\mac-vendor.csv";

    private readonly IConfiguration _configuration;

    public MachineDataSeeder(
        IConfiguration configuration,
        IMapper mapper,
        MachineDbContext dbContext,
        ILogger<MachineDataSeeder> logger
       ) : base(dbContext, mapper, logger)
    {
        _configuration = configuration;
    }

    protected override IEnumerable<MacAddressInfo> GetDataInfos()
    {
        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;

        var sourceFilePath = Path.Combine(sourceOutputPath, SourceFilePath);

        if (!File.Exists(sourceFilePath))
        {
            DownloadSourceFileAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        using var sr = new StreamReader(sourceFilePath);
        using var csvReader = new CsvReader(sr, CultureInfo.InvariantCulture);

        var dataRecords = new List<MacAddressInfo>();
        while (csvReader.Read())
        {
            var data = csvReader.GetRecord<MacAddressInfo>();
            if (data == null)
            {
                throw new NoxDataExtractorException($"Unable parse data obtained from {sourceFilePath}", sourceFilePath);
            }
            dataRecords.Add(data);
        }

        return dataRecords;
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
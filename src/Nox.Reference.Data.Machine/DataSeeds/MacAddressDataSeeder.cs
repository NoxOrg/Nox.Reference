using System.Globalization;
using AutoMapper;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;

namespace Nox.Reference.Data.Machine;

internal class MacAddressDataSeeder : NoxReferenceDataSeederBase<MachineDbContext, MacAddressInfo, MacAddress>
{
    private readonly IConfiguration _configuration;

    public MacAddressDataSeeder(
        IConfiguration configuration,
        IMapper mapper,
        MachineDbContext dbContext,
        ILogger<MacAddressDataSeeder> logger,
        NoxReferenceFileStorageService fileStorageService
       ) : base(dbContext, mapper, logger, fileStorageService)
    {
        _configuration = configuration;
    }

    public override string TargetFileName => "Nox.Reference.MacAddresses.json";

    public override string DataFolderPath => "MacAddresses";

    protected override List<MacAddressInfo> GetDataInfos()
    {
        var binaryData = DownloadSourceFileAsync()
              .ConfigureAwait(false)
              .GetAwaiter()
              .GetResult();

        // TODO: sort raw data before insert
        _fileStorageService.SaveContentToSource(binaryData, DataFolderPath, "mac-vendor.csv");

        using var ms = new MemoryStream(binaryData);
        using var sr = new StreamReader(ms);
        using var csvReader = new CsvReader(sr, CultureInfo.InvariantCulture);

        var dataRecords = new List<MacAddressInfo>();
        while (csvReader.Read())
        {
            var data = csvReader.GetRecord<MacAddressInfo>();
            if (data == null)
            {
                throw new NoxDataExtractorException($"Unable parse data");
            }
            dataRecords.Add(data);
        }

        return dataRecords.OrderBy(x => x.IEEERegistry).ThenBy(x => x.Id).ToList();
    }

    private async Task<byte[]> DownloadSourceFileAsync()
    {
        var uriMacAddresses = _configuration.GetValue<string>(ConfigurationConstants.UriMacAddressesSettingName)!;

        using var httpClient = new HttpClient();

        using var stream = await httpClient.GetStreamAsync(uriMacAddresses);
        using var ms = new MemoryStream();

        await stream.CopyToAsync(ms);

        return ms.ToArray();
    }
}
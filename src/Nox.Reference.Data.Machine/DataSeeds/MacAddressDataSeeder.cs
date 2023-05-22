using AutoMapper;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;
using System.Globalization;
using System.Text;

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

        using var ms = new MemoryStream(binaryData);
        var rawData = Encoding.ASCII.GetString(ms.ToArray());
        var splitRows = rawData.Split("\n");
        var sortedRows = splitRows.Skip(1).OrderBy(x => x).Skip(1).ToList();
        sortedRows.Insert(0, splitRows[0]);
        var resultingString = string.Join("\n", sortedRows);
        // TODO: fix saved data
        _fileStorageService.SaveContentToSource(resultingString, DataFolderPath, "mac-vendor.csv");

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
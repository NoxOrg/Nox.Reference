using System.Globalization;
using AutoMapper;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;

namespace Nox.Reference.MacAddresses.DataContext;

internal class MacAddressDataSeeder : INoxReferenceDataSeeder
{
    private const string SourceFilePath = @"MacAddresses\mac-vendor.csv";

    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly MacAddressDbContext _dbContext;

    //private readonly ILogger<MacAddressDataSeeder> _logger;

    public MacAddressDataSeeder(
        IConfiguration configuration,
        IMapper mapper,
        MacAddressDbContext dbContext
       //ILogger<MacAddressDataSeeder> logger
       )
    {
        _configuration = configuration;
        _mapper = mapper;
        _dbContext = dbContext;
        // _logger = logger;
    }

    public void Seed()
    {
        var dataSet = _dbContext
            .Set<MacAddress>();

        if (dataSet.Any())
        {
            return;
        }

        //_logger.LogInformation("Getting MAC Address data...");

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
        var entities = _mapper.Map<IEnumerable<MacAddress>>(dataRecords);
        dataSet.AddRange(entities);

        _dbContext.SaveChanges();
        // _logger.LogInformation("Getting MAC Address data successfuly completed...");
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
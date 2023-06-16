using System.Globalization;
using AutoMapper;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;

namespace Nox.Reference.Data.IpAddress.DataSeeds;

internal class IpAddressDataSeeder : NoxReferenceDataSeederBase<IpAddressDbContext, Reference.IpAddressInfo, IpAddress>
{
    private readonly IConfiguration _configuration;

    public IpAddressDataSeeder(
        IConfiguration configuration,
        IpAddressDbContext dbContext,
        IMapper mapper,
        ILogger<IpAddressDataSeeder> logger,
        NoxReferenceFileStorageService fileStorageService)
        : base(dbContext, mapper, logger, fileStorageService, false)
    {
        _configuration = configuration;
    }

    public override string TargetFileName => "Nox.Reference.IpAddresses.json";

    public override string DataFolderPath => "IpAddresses";

    protected override IReadOnlyList<Reference.IpAddressInfo> GetFlatEntitiesFromDataSources()
    {
        var ip4Url = _configuration.GetValue<string>(ConfigurationConstants.Ip4AddressesUrlSettingName)!;
        var ip6Url = _configuration.GetValue<string>(ConfigurationConstants.Ip6AddressesUrlSettingName)!;

        var ipAddresses = new List<Reference.IpAddressInfo>();

        var ip4Addresses = GetData(ip4Url);
        var ip6Addresses = GetData(ip6Url);

        ipAddresses.AddRange(ip4Addresses);
        ipAddresses.AddRange(ip6Addresses);

        return ipAddresses;
    }

    private static IEnumerable<Reference.IpAddressInfo> GetData(string url)
    {
        using var httpClient = new HttpClient();

        var response = httpClient.GetAsync(url).ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        var inputStream = response.Content.ReadAsStream();

        using var sr = new StreamReader(inputStream);
        using var csvReader = new CsvReader(sr, CultureInfo.InvariantCulture);
        var ipAddresses = new List<Reference.IpAddressInfo>();

        while (csvReader.Read())
        {
            var ipAddress = new Reference.IpAddressInfo
            {
                StartAddress = csvReader[0]!,
                EndAddress = csvReader[1]!,
                CountryCode = csvReader[2]!
            };
            ipAddresses.Add(ipAddress);
        }

        return ipAddresses;
    }
}
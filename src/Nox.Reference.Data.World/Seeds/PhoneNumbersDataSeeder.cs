using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;

namespace Nox.Reference.Data.World;

internal class PhoneNumbersDataSeeder : NoxReferenceDataSeederBase<WorldDbContext, PhoneCarrierInfo, PhoneCarrier>
{
    private readonly IConfiguration _configuration;

    public PhoneNumbersDataSeeder(WorldDbContext dbContext,
        IConfiguration configuration,
        IMapper mapper,
        ILogger<PhoneNumbersDataSeeder> logger,
        NoxReferenceFileStorageService fileStorageService)
        : base(dbContext, mapper, logger, fileStorageService)
    {
        _configuration = configuration;
    }

    public override string TargetFileName => "Nox.Reference.PhoneNumberCarriers.json";

    public override string DataFolderPath => "PhoneNumberCarriers";

    protected override IReadOnlyList<PhoneCarrierInfo> GetFlatEntitiesFromDataSources()
    {
        var phoneCarrierDataPath = _configuration.GetValue<string>(ConfigurationConstants.PhoneCarrierDataPathSettingName)!;

        var content = File.ReadAllText(phoneCarrierDataPath);

        var deserializationOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var data = JsonSerializer.Deserialize<List<PhoneCarrierInfo>>(content, deserializationOptions)!;

        return data;
    }
}
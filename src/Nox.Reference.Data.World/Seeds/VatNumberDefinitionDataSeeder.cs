using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;
using System.Text.Json;

namespace Nox.Reference.Data.World;

internal class VatNumberDefinitionDataSeeder : NoxReferenceDataSeederBase<WorldDbContext, VatNumberDefinitionInfo, VatNumberDefinition>
{
    private readonly IConfiguration _configuration;

    public VatNumberDefinitionDataSeeder(
        IConfiguration configuration,
        IMapper mapper,
        WorldDbContext dbContext,
        ILogger<VatNumberDefinitionDataSeeder> logger,
        NoxReferenceFileStorageService noxReferenceFileStorage)
        : base(dbContext, mapper, logger, noxReferenceFileStorage)
    {
        _configuration = configuration;
    }

    public override string TargetFileName => "Nox.Reference.VatNumberDefinitions.json";

    public override string DataFolderPath => "VatNumberDefinitions";

    protected override IEnumerable<VatNumberDefinitionInfo> GetDataInfos()
    {
        var vatNumberDefinitionDataPath = _configuration.GetValue<string>(ConfigurationConstants.VatNumberDefinitionDataPathSettingName)!;

        var content = File.ReadAllText(vatNumberDefinitionDataPath);
        var data = JsonSerializer.Deserialize<VatNumberDefinitionInfo[]>(content)!;

        return data;
    }
}
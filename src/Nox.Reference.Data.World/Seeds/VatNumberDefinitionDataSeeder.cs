using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;
using Nox.Reference.Data.World.Models;
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

    protected override IReadOnlyList<VatNumberDefinitionInfo> GetFlatEntitiesFromDataSources()
    {
        var vatNumberDefinitionDataPath = _configuration.GetValue<string>(ConfigurationConstants.VatNumberDefinitionDataPathSettingName)!;

        var content = File.ReadAllText(vatNumberDefinitionDataPath);
        var data = JsonSerializer.Deserialize<List<VatNumberDefinitionInfo>>(content)!;

        return data;
    }

    protected override void DoSpecialTreatAfterAdding(IEnumerable<VatNumberDefinitionInfo> sources, IEnumerable<VatNumberDefinition> destinations)
    {
        base.DoSpecialTreatAfterAdding(sources, destinations);

        var countries = _dbContext.Set<Country>().ToList();

        foreach (var source in sources)
        {
            var vatNumberDefinitionEntity = destinations.First(x => x.CountryCode == source.Country);
            vatNumberDefinitionEntity.Country = countries.First(x => source.Country == x.AlphaCode2);
            vatNumberDefinitionEntity.Country.VatNumberDefinition = vatNumberDefinitionEntity;
        }

        _dbContext.Set<VatNumberDefinition>()
            .UpdateRange(destinations);

        _dbContext.SaveChanges();
    }
}
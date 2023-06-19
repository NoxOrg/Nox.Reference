using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;
using System.Text.Json;

namespace Nox.Reference.Data.World;

internal class TaxNumberDefinitionDataSeeder : NoxReferenceDataSeederBase<WorldDbContext, TaxNumberDefinitionInfo, TaxNumberDefinition>
{
    private readonly IConfiguration _configuration;

    public TaxNumberDefinitionDataSeeder(
        IConfiguration configuration,
        IMapper mapper,
        WorldDbContext dbContext,
        ILogger<TaxNumberDefinitionDataSeeder> logger,
        NoxReferenceFileStorageService noxReferenceFileStorage)
        : base(dbContext, mapper, logger, noxReferenceFileStorage)
    {
        _configuration = configuration;
    }

    public override string TargetFileName => "Nox.Reference.TaxNumberDefinitions.json";

    public override string DataFolderPath => "TaxNumberDefinitions";

    protected override IReadOnlyList<TaxNumberDefinitionInfo> GetFlatEntitiesFromDataSources()
    {
        var taxNumberDefinitionDataPath = _configuration.GetValue<string>(ConfigurationConstants.TaxNumberDefinitionDataPathSettingName)!;

        var content = File.ReadAllText(taxNumberDefinitionDataPath);
        var data = JsonSerializer.Deserialize<List<TaxNumberDefinitionInfo>>(content)!;

        return data;
    }

    protected override void DoSpecialTreatAfterAdding(IEnumerable<TaxNumberDefinitionInfo> sources, IEnumerable<TaxNumberDefinition> destinations)
    {
        base.DoSpecialTreatAfterAdding(sources, destinations);

        var countries = _dbContext.Set<Country>().ToList();

        foreach (var source in sources)
        {
            var taxNumberDefinitionEntity = destinations.First(x => x.CountryCode == source.Country);
            taxNumberDefinitionEntity.Country = countries.First(x => source.Country == x.AlphaCode2);
            taxNumberDefinitionEntity.Country.TaxNumberDefinition = taxNumberDefinitionEntity;
        }

        _dbContext.Set<TaxNumberDefinition>()
            .UpdateRange(destinations);

        _dbContext.SaveChanges();
    }
}
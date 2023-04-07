using Microsoft.Extensions.Configuration;
using Nox.Reference.Countries;

namespace Nox.Reference.GetData.DataSeeds;

public class CountryFileSeed : JsonFileDataSeedBase<ICountryInfo>
{
    private const string OutputFilePath = "Nox.Reference.Countries.json";

    public CountryFileSeed(IConfiguration configuration) : base(configuration, OutputFilePath)
    {
    }
}
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.GetData.DataSeeds;

public class CurrencyFileSeed : JsonFileDataSeedBase<ICurrencyInfo>
{
    private const string OutputFilePath = "Nox.Reference.Currencies.json";

    public CurrencyFileSeed(IConfiguration configuration) : base(configuration, OutputFilePath)
    {
    }
}
using Microsoft.Extensions.Logging;
using Nox.Reference.Data.Common;

namespace Nox.Reference.DataLoaders;

internal class DataSeedRunner
{
    private readonly IEnumerable<INoxReferenceDataSeeder> _dataSeeders;
    private readonly ILogger<DataSeedRunner> _logger;

    public DataSeedRunner(IEnumerable<INoxReferenceDataSeeder> dataSeeders,
        ILogger<DataSeedRunner> logger)
    {
        _dataSeeders = dataSeeders;
        _logger = logger;
    }

    public void Run()
    {
        foreach (var dataSeeder in _dataSeeders)
        {
            try
            {
                dataSeeder.Seed();
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "Exception happened during seeding data {dataSeeder}. Exception: {Message}",
                    dataSeeder.GetType().Name,
                    e.Message);
            }
        }
    }
}
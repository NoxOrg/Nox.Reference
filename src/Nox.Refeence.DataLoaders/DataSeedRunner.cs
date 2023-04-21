using Nox.Reference.Data.Common;

namespace Nox.Reference.DataLoaders;

public class DataSeedRunner
{
    private readonly IEnumerable<INoxReferenceDataSeeder> _dataSeeders;

    public DataSeedRunner(IEnumerable<INoxReferenceDataSeeder> dataSeeders)
    {
        _dataSeeders = dataSeeders;
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
                Console.WriteLine(e);
            }
        }
    }
}
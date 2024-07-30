using System.Linq;
using System.Threading.Tasks;

namespace Nox.Reference.Data.World.Tests;

public class WorldContextTests
{
    public WorldContext? _worldContext;

    [SetUp]
    public void Setup()
    {
        _worldContext = new WorldContext(DatabaseConstant.WorldDbPath);
    }

    [Test]
    public void WhenGetCountries_ShouldLoad()
    {
        var countries = _worldContext!.GetCountriesQuery().ToList();
        Assert.That(countries, Is.Not.Empty);
        Assert.That(countries.Count, Is.EqualTo(249));
    }

    [Test]
    public void WhenInMultiThreadContext_ShouldLoad()
    {
        Parallel.For(0, 100,i =>
        {
            using var localWorldContext = new WorldContext(DatabaseConstant.WorldDbPath);

            var countries = localWorldContext!.GetCountriesQuery().ToList();
            Assert.That(countries, Is.Not.Empty);
            Assert.That(countries.Count, Is.EqualTo(249));
        });
    }


    [TearDown]
    public void EndTest()
    {
        _worldContext?.Dispose();
    }
}

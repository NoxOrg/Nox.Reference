using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.World.Extensions.Queries;

namespace Nox.Reference.Data.World.Tests;

public class CountryInfoTests
{
    private IWorldInfoContext _worldDbContext = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _worldDbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [Test]
    public void CountryInfo_WithIso3Alpha_ReturnsValidInfo()
    {
        var info = _worldDbContext.Countries.Get("ZAF")!;
        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Code, Is.EqualTo("ZAF"));
            Assert.That(info.Languages, Is.Not.Empty);
        });
    }

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.World.Extensions.Queries;
using System.Diagnostics;

namespace Nox.Reference.Data.World.Tests;

public class TimeZonesTests
{
    // set during mamndatory init
    private IWorldInfoContext _worldDbContext = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        WorldDbContext.UseDatabasePath(DatabaseConstant.WorldDbPath);
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _worldDbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    #region GetCultures

    [Test]
    public void GetTimeZones_WithKnownGMTCode_ReturnsValidInfo()
    {
        var info = _worldDbContext.TimeZones.Get("Europe/Vilnius");

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(info));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.Type, Is.EqualTo("Canonical"));
            Assert.That(info?.Countries.Count, Is.EqualTo(1));
        });
    }

    #endregion GetCultures

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
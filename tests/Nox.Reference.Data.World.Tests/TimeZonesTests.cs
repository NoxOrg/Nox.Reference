using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.World.Extensions.Queries;
using System.Diagnostics;
using System.Text.Json;

namespace Nox.Reference.Data.World.Tests;

public class TimeZonesTests
{
    // set during mamndatory init
    private IWorldInfoContext _countryDbContext = null!;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    [OneTimeSetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _countryDbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    #region GetCultures

    [Test]
    public void GetTimeZones_WithKnownGMTCode_ReturnsValidInfo()
    {
        var info = _countryDbContext.TimeZones.Get("Europe/Vilnius");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.Type, Is.EqualTo("Canonical"));
        });
    }

    #endregion GetCultures

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Extensions.Queries;
using Nox.Reference.Data.World.Models;
using System.Diagnostics;

namespace Nox.Reference.Data.World.Tests;

public class TimeZonesTests
{
    // set during mamndatory init
    private IWorldInfoContext _worldDbContext = null!;

    public IMapper _mapper = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        WorldDbContext.UseDatabaseConnectionString(DatabaseConstant.WorldDbPath);
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _worldDbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    #region GetTimeZones

    [Test]
    public void GetTimeZones_WithKnownVilniusTimezone_ReturnsValidInfo()
    {
        var timeZone = _worldDbContext.TimeZones.Get("Europe/Vilnius")!;
        var timeZoneInfo = timeZone.ToDto();

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(timeZoneInfo));

        Assert.Multiple(() =>
        {
            Assert.That(timeZoneInfo, Is.Not.Null);
            Assert.That(timeZoneInfo?.Id, Is.EqualTo("Europe/Vilnius"));
            Assert.That(timeZoneInfo?.Type, Is.EqualTo("Canonical"));
            Assert.That(timeZoneInfo?.CountriesWithTimeZone.Count, Is.EqualTo(1));
        });
    }

    #endregion GetTimeZones

    #region GetTimeZoneByCoordinates

    [Test]
    public void GetTimeZoneByCoordinates_WithKnownUkraineCoords_ReturnsValidInfo()
    {
        var timeZone = TimeZone.GetTimeZoneByCoordinates(new GeoCoordinatesInfo
        {
            Latitude = 50.0196769m,
            Longitude = 36.3569638m
        })!;
        var timeZoneInfo = timeZone.ToDto();

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(timeZoneInfo));

        Assert.Multiple(() =>
        {
            Assert.That(timeZoneInfo, Is.Not.Null);
            Assert.That(timeZoneInfo.Id, Is.EqualTo("Europe/Kyiv"));
            Assert.That(timeZoneInfo.CountriesWithTimeZone.Count, Is.EqualTo(1));
        });
    }

    #endregion GetTimeZoneByCoordinates

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
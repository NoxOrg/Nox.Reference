using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.World.Extensions.Queries;
using Nox.Reference.Data.World.Tests.Mapping;
using Nox.Reference.Data.World.Tests.Models;
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
        WorldDbContext.UseDatabasePath(DatabaseConstant.WorldDbPath);
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _worldDbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(TimeZoneMapping));
        });
        _mapper = mapperConfiguration.CreateMapper();
    }

    #region GetTimeZones

    [Test]
    public void GetTimeZones_WithKnownVilniusTimezone_ReturnsValidInfo()
    {
        var info = _worldDbContext.TimeZones.Get("Europe/Vilnius");
        var mappedInfo = _mapper.Map<TimeZoneFlatModel>(info);

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(info));

        Assert.Multiple(() =>
        {
            Assert.That(mappedInfo, Is.Not.Null);
            Assert.That(mappedInfo?.Type, Is.EqualTo("Canonical"));
            Assert.That(mappedInfo?.Code, Is.EqualTo("Europe/Vilnius"));
            Assert.That(mappedInfo?.Countries.Count, Is.EqualTo(1));
        });
    }

    #endregion GetCultures

    #region GetTimeZoneByCoordinates

    [Test]
    public void GetTimeZoneByCoordinates_WithKnownUkraineCoords_ReturnsValidInfo()
    {
        var info = TimeZone.GetTimeZoneByCoordinates(new GeoCoordinatesInfo
        {
            Latitude = 50.0196769m,
            Longitude = 36.3569638m
        });
        var mappedInfo = _mapper.Map<TimeZoneFlatModel>(info);

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(mappedInfo));

        Assert.Multiple(() =>
        {
            Assert.That(mappedInfo, Is.Not.Null);
            Assert.That(mappedInfo?.Code, Is.EqualTo("Europe/Kyiv"));
            Assert.That(mappedInfo?.Countries.Count, Is.EqualTo(1));
        });
    }

    #endregion

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
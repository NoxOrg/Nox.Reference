using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.World.Extensions.Queries;
using Nox.Reference.Data.World.Models;
using Nox.Reference.World;
using System.Diagnostics;

namespace Nox.Reference.Data.World.Tests;

public class HolidayTests
{
    private IWorldInfoContext _worldDbContext = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        WorldDbContext.UseDatabasePath(DatabaseConstant.WorldDbPath);
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _worldDbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    #region GetHolidays

    [Test]
    public void GetHolidays_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var countryHolidayInfo = _worldDbContext.Holidays.Get(2023, "UA");

        var mappedHolidayInfo = World.Mapper.Map<CountryHolidayInfo>(countryHolidayInfo);

        Assert.Multiple(() =>
        {
            Assert.That(mappedHolidayInfo, Is.Not.Null);
            Assert.That(mappedHolidayInfo?.Country, Is.EqualTo("UA"));
            Assert.That(mappedHolidayInfo?.Holidays.Count, Is.EqualTo(17));
        });
    }

    [Test]
    public void GetHolidays_WithEnumUkraineCode_ReturnsValidInfo()
    {
        var countryHolidayInfo = _worldDbContext.Holidays.Get(2023, WorldCountries.Ukraine);

        var mappedHolidayInfo = World.Mapper.Map<CountryHolidayInfo>(countryHolidayInfo);

        Assert.Multiple(() =>
        {
            Assert.That(mappedHolidayInfo, Is.Not.Null);
            Assert.That(mappedHolidayInfo?.Country, Is.EqualTo("UA"));
            Assert.That(mappedHolidayInfo?.Holidays.Count, Is.EqualTo(17));
        });
    }

    #endregion GetHolidays

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
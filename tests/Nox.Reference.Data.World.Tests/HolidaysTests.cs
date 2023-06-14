using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Nox.Reference.Data.World.Tests;

public class HolidayTests
{
    private IWorldInfoContext _worldDbContext = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        WorldDbContext.UseDatabaseConnectionString(DatabaseConstant.WorldDbPath);
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _worldDbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    #region GetHolidays

    [Test]
    public void GetHolidays_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var countryHoliday = _worldDbContext.Holidays.Get(2023, "UA")!;

        var countryHolidayInfo = countryHoliday.ToDto();

        Assert.Multiple(() =>
        {
            Assert.That(countryHolidayInfo, Is.Not.Null);
            Assert.That(countryHolidayInfo?.Country, Is.EqualTo("UA"));
            Assert.That(countryHolidayInfo?.Holidays.Count, Is.EqualTo(17));
        });
    }

    [Test]
    public void GetHolidays_WithEnumUkraineCode_ReturnsValidInfo()
    {
        var countryHoliday = _worldDbContext.Holidays.Get(2023, WorldCountries.Ukraine)!;
        var countryHolidayInfo = countryHoliday.ToDto();

        Assert.Multiple(() =>
        {
            Assert.That(countryHolidayInfo, Is.Not.Null);
            Assert.That(countryHolidayInfo.Country, Is.EqualTo("UA"));
            Assert.That(countryHolidayInfo.Holidays.Count, Is.EqualTo(17));
        });
    }

    #endregion GetHolidays

    #region GetHoliday

    [TestCase("UA", "2023-01-04", null, null, null)]
    [TestCase("UA", "2023-01-02", null, null, "New Year")]
    [TestCase("US", "2023-02-20", "AL", null, "George Washington/Thomas Jefferson Birthday")]
    [TestCase("US", "2023-02-20", "Alabama", null, "George Washington/Thomas Jefferson Birthday")]
    [TestCase("VE", "2023-01-06", "K", "B", "Epiphany")]
    [TestCase("VE", "2023-01-06", "Lara", "Anzoátegui", "Epiphany")]
    public void GetHoliday_PositiveAndNegativeScenarios(
        string country,
        string date,
        string state,
        string region,
        string expectedName)
    {
        var countryHoliday = _worldDbContext.Holidays.GetHoliday(country, DateTime.Parse(date), state, region)!;

        Assert.Multiple(() =>
        {
            if (string.IsNullOrWhiteSpace(expectedName))
            {
                Assert.That(countryHoliday, Is.Null);
            }
            else
            {
                Assert.That(countryHoliday, Is.Not.Null);
                Assert.That(countryHoliday.Name, Is.EqualTo(expectedName));
            }
        });
    }

    [TestCase(WorldCountries.Ukraine, "2023-01-04", null, null, null)]
    [TestCase(WorldCountries.Ukraine, "2023-01-02", null, null, "New Year")]
    [TestCase(WorldCountries.UnitedStates, "2023-02-20", "AL", null, "George Washington/Thomas Jefferson Birthday")]
    [TestCase(WorldCountries.UnitedStates, "2023-02-20", "Alabama", null, "George Washington/Thomas Jefferson Birthday")]
    [TestCase(WorldCountries.Venezuela, "2023-01-06", "K", "B", "Epiphany")]
    [TestCase(WorldCountries.Venezuela, "2023-01-06", "Lara", "Anzoátegui", "Epiphany")]
    public void GetHoliday_PositiveAndNegativeScenarios_WithEnumCode(
        WorldCountries country,
        string date,
        string state,
        string region,
        string expectedName)
    {
        var countryHoliday = _worldDbContext.Holidays.GetHoliday(country, DateTime.Parse(date), state, region)!;

        Assert.Multiple(() =>
        {
            if (string.IsNullOrWhiteSpace(expectedName))
            {
                Assert.That(countryHoliday, Is.Null);
            }
            else
            {
                Assert.That(countryHoliday, Is.Not.Null);
                Assert.That(countryHoliday.Name, Is.EqualTo(expectedName));
            }
        });
    }

    #endregion GetHoliday

    #region IsHolidayDate

    [TestCase("UA", "2023-01-04", null, null, false)]
    [TestCase("UA", "2023-01-02", null, null, true)]
    [TestCase("US", "2023-02-20", "AL", null, true)]
    [TestCase("US", "2023-02-20", "Alabama", null, true)]
    [TestCase("VE", "2023-01-06", "K", "B", true)]
    [TestCase("VE", "2023-01-06", "Lara", "Anzoátegui", true)]
    public void IsHolidayDate_PositiveAndNegativeScenarios(
        string country,
        string date,
        string state,
        string region,
        bool expectedResult)
    {
        var isHoliday = _worldDbContext.Holidays.IsHolidayDate(country, DateTime.Parse(date), state, region)!;

        Assert.That(isHoliday, Is.EqualTo(expectedResult));
    }

    [TestCase(WorldCountries.Ukraine, "2023-01-04", null, null, false)]
    [TestCase(WorldCountries.Ukraine, "2023-01-02", null, null, true)]
    [TestCase(WorldCountries.UnitedStates, "2023-02-20", "AL", null, true)]
    [TestCase(WorldCountries.UnitedStates, "2023-02-20", "Alabama", null, true)]
    [TestCase(WorldCountries.Venezuela, "2023-01-06", "K", "B", true)]
    [TestCase(WorldCountries.Venezuela, "2023-01-06", "Lara", "Anzoátegui", true)]
    public void IsHolidayDate_PositiveAndNegativeScenarios_WithEnumCode(
        WorldCountries country,
        string date,
        string state,
        string region,
        bool expectedResult)
    {
        var isHoliday = _worldDbContext.Holidays.IsHolidayDate(country, DateTime.Parse(date), state, region)!;

        Assert.That(isHoliday, Is.EqualTo(expectedResult));
    }

    #endregion IsHolidayDate

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
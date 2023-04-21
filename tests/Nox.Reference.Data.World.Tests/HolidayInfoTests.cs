using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using Nox.Reference.Holidays;

namespace Nox.Reference.Data.World.Tests;

public class HolidayInfoTests
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    [OneTimeSetUp]
    public void Setup()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    #region Constructor

    [Test]
    public void Constructor_WithNOtGeneratedYearData_ThrowsArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            new HolidaysService(1999);
        });

        Trace.WriteLine($"An exception was thrown: {exception!.Message}");
    }

    #endregion Constructor

    #region GetHolidays

    [Test]
    public void GetHolidays_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var currencyInfoService = new HolidaysService(DateTime.Now.Year);
        var info = currencyInfoService.GetHolidays().HolidaysByCountries.First(x => x.Country.Equals("UA", System.StringComparison.InvariantCultureIgnoreCase));

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.Country, Is.EqualTo("UA"));
        });
    }

    #endregion GetHolidays

    #region GetHolidaysByCountryCode

    [Test]
    public void GetHolidaysByCountryCode_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var currencyInfoService = new HolidaysService(DateTime.Now.Year);
        var info = currencyInfoService.GetHolidaysByCountryCode("UA");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.Country, Is.EqualTo("UA"));
        });
    }

    [Test]
    public void GetHolidaysByCountryCode_WitUnknownCode_ReturnsNull()
    {
        var currencyInfoService = new HolidaysService(DateTime.Now.Year);
        var info = currencyInfoService.GetHolidaysByCountryCode("SomeCode");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Null);
        });
    }

    #endregion GetHolidaysByCountryCode

    #region GetHolidaysByCountryName

    [Test]
    public void GetHolidaysByCountryName_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var currencyInfoService = new HolidaysService(DateTime.Now.Year);
        var info = currencyInfoService.GetHolidaysByCountryName("Ukraine");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.CountryName, Is.EqualTo("Ukraine"));
        });
    }

    [Test]
    public void GetHolidaysByCountryName_WitUnknownCode_ReturnsNull()
    {
        var currencyInfoService = new HolidaysService(DateTime.Now.Year);
        var info = currencyInfoService.GetHolidaysByCountryName("SomeCode");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Null);
        });
    }

    #endregion GetHolidaysByCountryName

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
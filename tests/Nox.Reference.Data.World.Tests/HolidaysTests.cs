using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.World.Extensions.Queries;

namespace Nox.Reference.Data.World.Tests;

public class HolidayTests
{
    #region GetHolidays

    [Test]
    public void GetHolidays_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var countryHolidayInfo = WorldInfo.Holidays.Get(2023, "UA");
        Assert.Multiple(() =>
        {
            Assert.That(countryHolidayInfo, Is.Not.Null);
            Assert.That(countryHolidayInfo?.Country, Is.EqualTo("UA"));
        });
    }

    #endregion GetHolidays

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
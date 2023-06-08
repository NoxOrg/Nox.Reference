using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Extensions.Queries;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Nox.Reference.Data.World.Tests;

public class CulturesTests
{
    // set during mamndatory init
    private IWorldInfoContext _worldDbContext = null!;

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

    #region GetCultures

    [Test]
    public void GetCultures_WithKnownEnglishCode_ReturnsValidInfo()
    {
        var culture = _worldDbContext.Cultures.Get("en-US")!;

        var cultureInfo = culture.ToDto<Models.CultureInfo>();

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(cultureInfo));

        Assert.Multiple(() =>
        {
            Assert.That(cultureInfo, Is.Not.Null);
            Assert.That(cultureInfo.Country, Is.Not.Null);
            Assert.That(cultureInfo.DisplayName, Is.EqualTo("English (United States)"));
        });
    }

    [Test]
    public void GetCultures_GetByCountry_ReturnsValidInfo()
    {
        var countries = _worldDbContext.Cultures.GetByCountry(Reference.World.WorldCountries.UnitedStates);

        var mappedInfo = countries.ToDto<Models.CultureInfo>();

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(mappedInfo));

        Assert.Multiple(() =>
        {
            Assert.That(mappedInfo, Is.Not.Null);
            Assert.That(mappedInfo.Count, Is.EqualTo(6));
        });
    }

    #endregion GetCultures

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
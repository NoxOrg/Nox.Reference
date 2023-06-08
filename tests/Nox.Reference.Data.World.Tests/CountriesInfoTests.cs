using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Extensions.Queries;
using Nox.Reference.Data.World.Models;
using Nox.Reference.World;
using System.Diagnostics;

namespace Nox.Reference.Data.World.Tests;

public class CountryInfoTests
{
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

    [Test]
    public void CountryInfo_WithIso3Alpha_ReturnsValidInfo()
    {
        const string countryCode = "ZAF";
        var country = World.Countries.Get(countryCode)!;
        Assert.IsNotNull(country);
        Assert.That(country.Id, Is.EqualTo(countryCode));

        var countryInfo = country.ToDto<CountryInfo>();

        Assert.Multiple(() =>
        {
            Assert.That(countryInfo, Is.Not.Null);
            Assert.That(countryInfo.Code, Is.EqualTo(countryCode));
            Assert.That(countryInfo.Languages, Is.Not.Empty);
            Assert.That(countryInfo.NameTranslations, Is.Not.Empty);
            Assert.That(countryInfo.Currencies, Is.Not.Empty);
            Assert.That(countryInfo.TopLevelDomains, Is.Not.Empty);
            Assert.That(countryInfo.Capitals, Is.Not.Empty);
        });
    }

    [Test]
    public void CountryInfo_WithEnum_ReturnsValidInfo()
    {
        var country = World.Countries.Get(WorldCountries.SouthAfrica)!;

        var countryInfo = country.ToDto<CountryInfo>();

        Assert.Multiple(() =>
        {
            Assert.That(countryInfo, Is.Not.Null);
            Assert.That(countryInfo.Code, Is.EqualTo("ZAF"));
            Assert.That(countryInfo.Languages, Is.Not.Empty);
            Assert.That(countryInfo.NameTranslations, Is.Not.Empty);
            Assert.That(countryInfo.Currencies, Is.Not.Empty);
            Assert.That(countryInfo.TopLevelDomains, Is.Not.Empty);
            Assert.That(countryInfo.Capitals, Is.Not.Empty);
        });
    }

    [Test]
    public void CountryInfo_WithIso3AlphaAndTranslationForCountry_ReturnsTranslation()
    {
        var translation = _worldDbContext.Countries
            .Get("ZAF")!
            .GetTranslation("en")!;

        var translationInfo = translation.ToDto<CountryNameTranslationInfo>();

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(translationInfo));

        Assert.Multiple(() =>
        {
            Assert.That(translationInfo, Is.Not.Null);
            Assert.That(translationInfo.OfficialName, Is.EqualTo("South Africa"));
            Assert.That(translationInfo.CommonName, Is.EqualTo("South Africa"));
        });
    }

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
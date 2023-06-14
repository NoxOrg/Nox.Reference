using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using System;
using System.Diagnostics;

namespace Nox.Reference.Data.World.Tests;

public class CountryInfoTests
{
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

    [Test]
    public void CountryInfo_WithIso2Alpha_ReturnsValidInfo()
    {
        const string countryCode = "ZA";

        var country = Reference.World.Countries.Get(countryCode)!;
        Assert.IsNotNull(country);
        Assert.IsNotEmpty(country.Cultures);
        Assert.IsNotNull(country.VatNumberDefinition);
        Assert.That(country.Id, Is.EqualTo(countryCode));

        var countryInfo = country.ToDto();

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
        var country = Reference.World.Countries.Get(WorldCountries.SouthAfrica)!;

        var countryInfo = country.ToDto();

        Assert.Multiple(() =>
        {
            Assert.That(countryInfo, Is.Not.Null);
            Assert.That(countryInfo.Code, Is.EqualTo("ZA"));
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
            .Get("ZA")!
            .GetTranslation("en")!;

        var translationInfo = translation.ToDto();

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(translationInfo));

        Assert.Multiple(() =>
        {
            Assert.That(translationInfo, Is.Not.Null);
            Assert.That(translationInfo.OfficialName, Is.EqualTo("South Africa"));
            Assert.That(translationInfo.CommonName, Is.EqualTo("South Africa"));
        });
    }

    #region IsWorkingDay

    [TestCase("UA", "2023-06-19", true)]
    [TestCase("UA", "2023-06-18", false)]
    [TestCase("UA", "2023-06-17", false)]
    [TestCase("UA", "2023-06-16", true)]
    [TestCase("IL", "2023-06-18", true)]
    [TestCase("IL", "2023-06-17", false)]
    [TestCase("IL", "2023-06-16", false)]
    [TestCase("IL", "2023-06-15", true)]
    public void IsHolidayDate_PositiveAndNegativeScenarios(
        string country,
        string date,
        bool expectedResult)
    {
        var isWorkingDay = _worldDbContext.Countries.IsWorkingDay(country, DateTime.Parse(date))!;

        Assert.That(isWorkingDay, Is.EqualTo(expectedResult));
    }

    [TestCase(WorldCountries.Ukraine, "2023-06-19", true)]
    [TestCase(WorldCountries.Ukraine, "2023-06-18", false)]
    [TestCase(WorldCountries.Ukraine, "2023-06-17", false)]
    [TestCase(WorldCountries.Ukraine, "2023-06-16", true)]
    [TestCase(WorldCountries.Israel, "2023-06-18", true)]
    [TestCase(WorldCountries.Israel, "2023-06-17", false)]
    [TestCase(WorldCountries.Israel, "2023-06-16", false)]
    [TestCase(WorldCountries.Israel, "2023-06-15", true)]
    public void IsHolidayDate_PositiveAndNegativeScenarios_WithEnumCode(
        WorldCountries country,
        string date,
        bool expectedResult)
    {
        var isWorkingDay = _worldDbContext.Countries.IsWorkingDay(country, DateTime.Parse(date))!;

        Assert.That(isWorkingDay, Is.EqualTo(expectedResult));
    }

    #endregion IsWorkingDay

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
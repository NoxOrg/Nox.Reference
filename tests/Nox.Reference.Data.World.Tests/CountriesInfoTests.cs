using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
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
        var info = World.Countries.Get(countryCode)!;
        Assert.IsNotNull(info);
        Assert.IsNotEmpty(info.Cultures);
        Assert.IsNotNull(info.VatNumberDefinition);
        Assert.That(info.Id, Is.EqualTo(countryCode));

        var mappedInfo = World.Mapper.Map<CountryInfo>(info);

        Assert.Multiple(() =>
        {
            Assert.That(mappedInfo, Is.Not.Null);
            Assert.That(mappedInfo.Code, Is.EqualTo(countryCode));
            Assert.That(mappedInfo.Languages, Is.Not.Empty);
            Assert.That(mappedInfo.NameTranslations, Is.Not.Empty);
            Assert.That(mappedInfo.Currencies, Is.Not.Empty);
            Assert.That(mappedInfo.TopLevelDomains, Is.Not.Empty);
            Assert.That(mappedInfo.Capitals, Is.Not.Empty);
        });
    }

    [Test]
    public void CountryInfo_WithEnum_ReturnsValidInfo()
    {
        var info = World.Countries.Get(WorldCountries.SouthAfrica)!;

        var mappedInfo = World.Mapper.Map<CountryInfo>(info);

        Assert.Multiple(() =>
        {
            Assert.That(mappedInfo, Is.Not.Null);
            Assert.That(mappedInfo.Code, Is.EqualTo("ZA"));
            Assert.That(mappedInfo.Languages, Is.Not.Empty);
            Assert.That(mappedInfo.NameTranslations, Is.Not.Empty);
            Assert.That(mappedInfo.Currencies, Is.Not.Empty);
            Assert.That(mappedInfo.TopLevelDomains, Is.Not.Empty);
            Assert.That(mappedInfo.Capitals, Is.Not.Empty);
        });
    }

    [Test]
    public void CountryInfo_WithIso3AlphaAndTranslationForCountry_ReturnsTranslation()
    {
        var translation = _worldDbContext.Countries
            .Get("ZA")!
            .GetTranslation("en")!;

        var mappedTranslation = World.Mapper.Map<CountryNameTranslationInfo>(translation);

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(mappedTranslation));

        Assert.Multiple(() =>
        {
            Assert.That(mappedTranslation, Is.Not.Null);
            Assert.That(mappedTranslation.OfficialName, Is.EqualTo("South Africa"));
            Assert.That(mappedTranslation.CommonName, Is.EqualTo("South Africa"));
        });
    }

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
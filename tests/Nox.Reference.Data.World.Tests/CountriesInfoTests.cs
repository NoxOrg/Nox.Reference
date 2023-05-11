using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.World.Extensions.Queries;

namespace Nox.Reference.Data.World.Tests;

public class CountryInfoTests
{
    private IWorldInfoContext _worldDbContext = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _worldDbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [Test]
    public void CountryInfo_WithIso3Alpha_ReturnsValidInfo()
    {
        var info = WorldInfo.Countries.Get("ZAF")!;

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Code, Is.EqualTo("ZAF"));
            Assert.That(info.Languages, Is.Not.Empty);
            Assert.That(info.NameTranslations, Is.Not.Empty);
            Assert.That(info.Currencies, Is.Not.Empty);
            Assert.That(info.TopLevelDomains, Is.Not.Empty);
            Assert.That(info.Capitals, Is.Not.Empty);
        });
    }

    [Test]
    public void CountryInfo_WithIso3AlphaAndTranslationForCountry_ReturnsTranslation()
    {
        var translation = _worldDbContext.Countries.Get("ZAF")!.GetTranslation("en")!;

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(translation));

        Assert.Multiple(() =>
        {
            Assert.That(translation, Is.Not.Null);
            Assert.That(translation.OfficialName, Is.EqualTo("South Africa"));
            Assert.That(translation.CommonName, Is.EqualTo("South Africa"));
        });
    }

    [Test]
    public void CountryNameTranslation_WithIso3Alpha_ReturnsTranslation()
    {
        var translations = WorldInfo.GetCountryTranslationsForLanguage("en").ToList();

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(translations));

        Assert.That(translations, Is.Not.Empty);
    }

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
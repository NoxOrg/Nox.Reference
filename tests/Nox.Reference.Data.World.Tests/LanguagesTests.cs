using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Extensions.Queries;
using Nox.Reference.Data.World.Models;
using System.Diagnostics;

namespace Nox.Reference.Data.World.Tests;

public class LanguagesTests
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

    [TestCase("uk", "true", "Ukrainian", 4, 1, "UKR")]
    public void GetLanguages_ReturnsProperValue(
        string input,
        string expectedIsCommon,
        string expectedName,
        int expectedNameTranslationCount,
        int expectedCountryCount,
        string countryCode)
    {
        Language language = _worldDbContext.Languages.Get(input)!;
        Assert.That(language, Is.Not.Null);
        Assert.That(language.Id, Is.EqualTo("ukr"));

        var mappedInfo = language.ToDto<LanguageInfo>();

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(mappedInfo));

        Assert.That(mappedInfo, Is.Not.Null);
        Assert.That(mappedInfo.Common, Is.EqualTo(bool.Parse(expectedIsCommon)));
        Assert.That(mappedInfo.Name, Is.EqualTo(expectedName));
        Assert.That(mappedInfo.NameTranslations.Count, Is.EqualTo(expectedNameTranslationCount));
        Assert.That(mappedInfo.Countries.Count, Is.EqualTo(expectedCountryCount));
        Assert.That(mappedInfo.Countries[0], Is.EqualTo(countryCode));
    }
}
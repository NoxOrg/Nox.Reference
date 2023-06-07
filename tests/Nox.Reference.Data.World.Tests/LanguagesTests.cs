using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.World.Extensions.Queries;
using System.Diagnostics;

namespace Nox.Reference.Data.World.Tests;

public class LanguagesTests
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

    [TestCase("uk", "true", "Ukrainian", 4, 1, "UA")]
    public void GetLanguages_ReturnsProperValue(
        string input,
        string expectedIsCommon,
        string expectedName,
        int expectedNameTranslationCount,
        int expectedCountryCount,
        string countryCode)
    {
        var info = _worldDbContext.Languages.Get(input);
        Assert.That(info, Is.Not.Null);
        Assert.That(info!.Id, Is.EqualTo("ukr"));

        var mappedInfo = World.Mapper.Map<Models.LanguageInfo>(info);

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(mappedInfo));

        Assert.That(mappedInfo, Is.Not.Null);
        Assert.That(mappedInfo?.Common, Is.EqualTo(bool.Parse(expectedIsCommon)));
        Assert.That(mappedInfo?.Name, Is.EqualTo(expectedName));
        Assert.That(mappedInfo?.NameTranslations.Count, Is.EqualTo(expectedNameTranslationCount));
        Assert.That(mappedInfo?.Countries.Count, Is.EqualTo(expectedCountryCount));
        Assert.That(mappedInfo?.Countries[0], Is.EqualTo(countryCode));
    }
}
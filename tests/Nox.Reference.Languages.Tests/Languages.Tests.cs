using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Languages.Services;
using System.Diagnostics;
using System.Linq;

namespace Nox.Reference.Languages.Tests;

public class LanguagesTests
{
    private ILanguagesService _languagesService = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddNoxLanguages();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _languagesService = serviceProvider.GetRequiredService<ILanguagesService>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [TestCase("uk", "true", "Ukrainian")]
    public void GetLanguages_ReturnsProperValue(
        string input,
        string expectedIsCommon,
        string expectedName)
    {
        var info = _languagesService.GetLanguages().FirstOrDefault(x => input.Equals(x.Iso_639_1));

        Assert.That(info, Is.Not.Null);
        Assert.That(info?.Common, Is.EqualTo(bool.Parse(expectedIsCommon)));
        Assert.That(info?.EnglishName, Is.EqualTo(expectedName));
    }
}
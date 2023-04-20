//using Microsoft.Extensions.DependencyInjection;
//using System.Diagnostics;
//using System.Linq;

//namespace Nox.Reference.Languages.Tests;

//public class LanguagesTests
//{
//    private ILanguagesService _languagesService = null!;

//    [OneTimeSetUp]
//    public void Setup()
//    {
//        var serviceCollection = new ServiceCollection();
//        serviceCollection.AddNoxLanguages();

//        var serviceProvider = serviceCollection.BuildServiceProvider();

//        _languagesService = serviceProvider.GetRequiredService<ILanguagesService>();

//        Trace.Listeners.Add(new ConsoleTraceListener());
//    }

//    [TestCase("uk", "true", "Ukrainian", "4")]
//    public void GetLanguages_ReturnsProperValue(
//        string input,
//        string expectedIsCommon,
//        string expectedName,
//        string expectedCount)
//    {
//        var info = _languagesService.GetLanguages().FirstOrDefault(x => input.Equals(x.Iso_639_1));

//        Assert.That(info, Is.Not.Null);
//        Assert.That(info?.Common, Is.EqualTo(bool.Parse(expectedIsCommon)));
//        Assert.That(info?.Name, Is.EqualTo(expectedName));
//        Assert.That(info?.NameTranslations.Count, Is.EqualTo(int.Parse(expectedCount)));
//    }
//}
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.World.Extensions.Queries;
using System.Diagnostics;

namespace Nox.Reference.Data.World.Tests;

public class CulturesTests
{
    // set during mamndatory init
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

    #region GetCultures

    [Test]
    public void GetCultures_WithKnownEnglishCode_ReturnsValidInfo()
    {
        var info = _worldDbContext.Cultures.Get("en-US");

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(info));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.DisplayName, Is.EqualTo("English (United States)"));
        });
    }

    #endregion GetCultures

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
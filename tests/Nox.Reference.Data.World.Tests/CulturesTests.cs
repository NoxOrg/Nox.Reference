using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Extensions.Queries;
using System.Diagnostics;
using System.Globalization;

namespace Nox.Reference.Data.World.Tests;

public class CulturesTests
{
    // set during mamndatory init
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
            Assert.That(cultureInfo?.DisplayName, Is.EqualTo("English (United States)"));
        });
    }

    #endregion GetCultures

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
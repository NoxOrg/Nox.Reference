using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
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
        var info = _worldDbContext.Cultures.Get("en-US");

        var mappedInfo = World.Mapper.Map<Models.CultureInfo>(info);

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(mappedInfo));

        Assert.Multiple(() =>
        {
            Assert.That(mappedInfo, Is.Not.Null);
            Assert.That(mappedInfo?.DisplayName, Is.EqualTo("English (United States)"));
        });
    }

    #endregion GetCultures

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
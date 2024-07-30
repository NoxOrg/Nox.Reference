using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Text.Json;

namespace Nox.Reference.Data.World.Tests;

public class CurrencyTests
{
    // set during mamndatory init
    private IWorldInfoContext _worldDbContext = null!;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

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

    #region GetCurrencies

    [Test]
    public void GetCurrencies_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var info = _worldDbContext.Currencies.GetByIsoCode("UAH");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.IsoCode, Is.EqualTo("UAH"));
            Assert.That(info?.Id, Is.EqualTo("UAH"));
        });
    }

    [Test]
    public void GetCurrencies_WithEnumUkraine_ReturnsValidInfo()
    {
        var info = _worldDbContext.Currencies.Get(WorldCurrencies.UkrainianHryvnia);

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.IsoCode, Is.EqualTo("UAH"));
        });
    }

    [Test]
    public void GetCurrencies_StaticWithKnownUkraineCode_ReturnsValidInfo()
    {
        Currency uaCurrency = _worldDbContext.Currencies.Get("UAH")!;

        Assert.Multiple(() =>
        {
            Assert.That(uaCurrency, Is.Not.Null);
            Assert.That(uaCurrency?.IsoCode, Is.EqualTo("UAH"));
        });
    }

    [Test]
    public void GetCurrencies_StaticGetCurrencyWithReferenceEntity_ReturnsReferenceInfo()
    {
        var currency = _worldDbContext.Currencies.Get("USD")!;

        var currencyInfo = currency.ToDto();

        Assert.That(currencyInfo.Units.MajorCurrencyUnit.Name, Is.EqualTo("dollar"));
        Assert.That(currencyInfo.Units.MinorCurrencyUnit.MajorValue, Is.EqualTo(0.01m));
    }

    #endregion GetCurrencies

    #region GetCurrencyByIsoCode

    [Test]
    public void GetCurrencyByIsoCode_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var info = _worldDbContext.Currencies.GetByIsoCode("UAH");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.IsoCode, Is.EqualTo("UAH"));
        });
    }

    [Test]
    public void GetCurrencyByIsoCode_WitUnknownCode_ReturnsNull()
    {
        var info = _worldDbContext.Currencies.GetByIsoCode("SomeCode");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Null);
        });
    }

    #endregion GetCurrencyByIsoCode

    #region GetCurrencyByIsoNumber

    [Test]
    public void GetCurrencyByIsoNumber_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var info = _worldDbContext.Currencies.GetByIsoNumber("980");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.IsoCode, Is.EqualTo("UAH"));
        });
    }

    [Test]
    public void GetCurrencyByIsoNumber_WitUnknownCode_ReturnsNull()
    {
        var info = _worldDbContext.Currencies.GetByIsoNumber("SomeCode");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Null);
        });
    }

    #endregion GetCurrencyByIsoNumber

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
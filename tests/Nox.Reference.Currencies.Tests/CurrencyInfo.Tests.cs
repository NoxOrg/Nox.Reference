using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Country.DataContext;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using System.Text.Json;

namespace Nox.Reference.Currency.Tests;

public class CurrencyTests
{
    // set during mamndatory init
    private ICountryContext _countryDbContext = null!;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    [OneTimeSetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCountryDbContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _countryDbContext = serviceProvider.GetRequiredService<ICountryContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    #region GetCurrencies

    [Test]
    public void GetCurrencies_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var info = _countryDbContext.Currencies.GetByIsoCode("UAH");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.IsoCode, Is.EqualTo("UAH"));
        });
    }

    #endregion GetCurrencies

    #region GetCurrencyByIsoCode

    [Test]
    public void GetCurrencyByIsoCode_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var info = _countryDbContext.Currencies.GetByIsoCode("UAH");

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
        var info = _countryDbContext.Currencies.GetByIsoCode("SomeCode");

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
        var info = _countryDbContext.Currencies.GetByIsoNumber("980");

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
        var info = _countryDbContext.Currencies.GetByIsoNumber("SomeCode");

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
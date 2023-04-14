using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Currencies;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace Nox.Reference.Currency.Tests;

public class CurrencyTests
{
    private ICurrenciesService _currenciesService;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    [SetUp]
    public void Setup()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new List<KeyValuePair<string, string?>>()
            {
                    new KeyValuePair<string, string?>( "ConnectionStrings:noxreferencesConnection", @"Data Source=..\..\..\..\..\data\noxreferences.db")
            })
          .Build();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddNoxCurrencies(configuration);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _currenciesService = serviceProvider.GetRequiredService<ICurrenciesService>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    #region GetCurrencies

    [Test]
    public void GetCurrencies_WithKnownUkraineCode_ReturnsValidInfo()
    {
        var info = _currenciesService.GetCurrencies()
            .First(x => x.IsoCode.Equals("UAH", System.StringComparison.InvariantCultureIgnoreCase));

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
        var info = _currenciesService.GetCurrencyByIsoCode("UAH");

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
        var info = _currenciesService.GetCurrencyByIsoCode("SomeCode");

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
        var info = _currenciesService.GetCurrencyByIsoNumber("980");

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
        var info = _currenciesService.GetCurrencyByIsoNumber("SomeCode");

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
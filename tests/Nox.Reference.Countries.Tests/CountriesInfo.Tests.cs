using System.Diagnostics;
using System.Text.Json;

namespace Nox.Reference.Countries.Tests;

public class CountryInfoTests
{
    private readonly JsonSerializerOptions _jsonOptions = new() 
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    [SetUp]
    public void Setup()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [Test]
    public void CountryInfo_WithIso3Alpha_ReturnsValidInfo()
    {
        ICountriesService countryService = new CountriesService();

        ICountryInfo? info = countryService?.GetCountries()?.First(c => c.Code.Equals("ZAF"));

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info?.Code, Is.EqualTo("ZAF"));
        });
        
    }

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
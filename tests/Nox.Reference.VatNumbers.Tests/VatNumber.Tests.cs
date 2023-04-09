using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Holidays;
using Nox.Reference.VatNumbers.Models;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nox.Reference.VatNumbers.Tests;

public class Tests
{
    private string _testFilePath = string.Empty;
    // set during mamndatory init
    private static IVatNumberService _vatNumberService = null!;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };


    [OneTimeSetUp]
    public void Setup()
    {
        var path = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (!Directory.Exists(Path.Combine(path.FullName, ".git")))
        {
            // not found, in root
            if (path.Parent == null)
            {
                path = new DirectoryInfo(Directory.GetCurrentDirectory());
                break;
            }
            path = path.Parent;
        }

        _testFilePath = Path.Combine(path.FullName, "data/tests/VatNumbers/");
        Trace.Listeners.Add(new ConsoleTraceListener());
        _jsonOptions.Converters.Add(new JsonStringEnumConverter());

        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddNoxVatNumbers();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _vatNumberService = serviceProvider.GetRequiredService<IVatNumberService>();
    }

    #region ValidateVatNumber

    [Test]
    public void VatNumber_WithValidUkraineNumber_ReturnsSuccess()
    {
        var validationResult = _vatNumberService.ValidateVatNumber(new VatNumberInfo("UA", "0203654090"));

        Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.ValidationResult.IsValid, Is.EqualTo(true));
            Assert.That(validationResult.IsVerified, Is.EqualTo(true));
        });
    }

    [Test]
    public void VatNumber_WithValidUAPrefix_ReturnsSuccess()
    {
        var validationResult = _vatNumberService.ValidateVatNumber(new VatNumberInfo("UA", "UA0203654090"));

        Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.ValidationResult.IsValid, Is.EqualTo(true));
            Assert.That(validationResult.IsVerified, Is.EqualTo(true));
        });
    }

    [Test]
    public void VatNumber_WithInvalidUkraineValue_ReturnsFail()
    {
        var validationResult = _vatNumberService.ValidateVatNumber(new VatNumberInfo("UA", "123Test456"));

        Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.ValidationResult.IsValid, Is.EqualTo(false));
            Assert.That(validationResult.ValidationResult.ValidationErrors, Has.Count.EqualTo(2));
        });
    }

    [Test]
    public void VatNumber_WithNotFoundSanMarinoNumber_ReturnsInvalid()
    {
        var validationResult = _vatNumberService.ValidateVatNumber(new VatNumberInfo("SM", "123456"));

        Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.ValidationResult.IsValid, Is.EqualTo(true));
            Assert.That(validationResult.IsVerified, Is.EqualTo(false));
        });
    }

    #endregion

    #region PerCountry

    [Test]
    [TestCase("UA")]
    [TestCase("ZA")]
    [TestCase("CO")]
    [TestCase("CH")]
    [TestCase("BR")]
    [TestCase("GB")]
    [TestCase("IT")]
    [TestCase("FR")]
    [TestCase("DE")]
    [TestCase("ES")]
    [TestCase("NL")]
    [TestCase("MX")]
    [TestCase("IN")]
    [TestCase("CA")]
    [TestCase("BE")]
    [TestCase("AU")]
    [TestCase("PL")]
    [TestCase("PT")]
    [Parallelizable(ParallelScope.All)]
    public void VatNumber_TestNumberOfFailingVATPerCountry(string countryCode)
    {
        using var sr = new StreamReader($"{_testFilePath}/{countryCode}-VatNumbers.json");
        var testData = JsonSerializer.Deserialize<string[]>(sr.ReadToEnd());

        var failedVat = new System.Collections.Generic.List<string>();
        foreach (var vatNumber in testData!)
        {
            var vatNumberInfo = _vatNumberService.ValidateVatNumber(new VatNumberInfo(countryCode, vatNumber));
            if (!vatNumberInfo.ValidationResult.IsValid ||
                !vatNumberInfo.IsVerified)
            {
                failedVat.Add($"{vatNumber}:{string.Join(';', vatNumberInfo.ValidationResult.ValidationErrors)}");
            }
        }

        Trace.WriteLine($"Failed VAT count: {failedVat.Count}/{testData.Length}");
        Trace.WriteLine("Failed VAT:");
        Trace.WriteLine(JsonSerializer.Serialize(failedVat, _jsonOptions));
    }

    #endregion

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
using Nox.Reference.VatNumbers.Models;
using Nox.Reference.VatNumbers.Services;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nox.Reference.VatNumbers.Tests;

public class Tests
{
    private string _testFilePath = "../../../../../data/tests/VatNumbers/";

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };


    [OneTimeSetUp]
    public void Setup()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
        _jsonOptions.Converters.Add(new JsonStringEnumConverter());
    }

    #region ValidateVatNumber

    [Test]
    public void VatNumber_WithValidUkraineNumber_ReturnsSuccess()
    {
        var service = new VatValidationService();
        var validationResult = service.ValidateVatNumber(new VatNumbers.Models.VatNumber("0203654090", "UA"));

        Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.ValidationStatus, Is.EqualTo(ValidationStatus.Valid));
        });
    }

    [Test]
    public void VatNumber_WithValidUAPrefix_ReturnsSuccess()
    {
        var service = new VatValidationService();
        var validationResult = service.ValidateVatNumber(new VatNumbers.Models.VatNumber("UA0203654090", "UA"));

        Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.ValidationStatus, Is.EqualTo(ValidationStatus.Valid));
        });
    }

    [Test]
    public void VatNumber_WithInvalidUkraineValue_ReturnsFail()
    {
        var service = new VatValidationService();
        var validationResult = service.ValidateVatNumber(new VatNumbers.Models.VatNumber("123Test456", "UA"));

        Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.ValidationStatus, Is.EqualTo(ValidationStatus.Invalid));
            Assert.That(validationResult.ValidationErrors, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void VatNumber_WithNotFoundSanMarinoNumber_ReturnsInvalid()
    {
        var service = new VatValidationService();
        var validationResult = service.ValidateVatNumber(new VatNumbers.Models.VatNumber("123456", "SM"));

        Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.ValidationStatus, Is.EqualTo(ValidationStatus.Invalid));
            Assert.That(validationResult.ValidationErrors[0], Is.EqualTo("Cannot find validator for a particular error."));
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
    [Parallelizable(ParallelScope.All)]
    public void VatNumber_TestNumberOfFailingVATPerCountry(string countryCode)
    {
        var service = new VatValidationService();
        using var sr = new StreamReader($"{_testFilePath}{countryCode}-VatNumbers.json");
        var testData = JsonSerializer.Deserialize<string[]>(sr.ReadToEnd());

        var failedVat = new System.Collections.Generic.List<string>();
        foreach (var vatNumber in testData!)
        {
            var validationResult = service.ValidateVatNumber(new VatNumbers.Models.VatNumber(vatNumber, countryCode));
            if (validationResult.ValidationStatus == ValidationStatus.Invalid)
            {
                failedVat.Add(vatNumber);
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
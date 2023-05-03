using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Abstractions;
using Nox.Reference.Data.World.Extensions.Queries;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Nox.Reference.Data.World.Tests;

//TODO: transform into a command-like structure similarly to get data project
public class VatNumberTests
{
    private string _testFilePath = string.Empty;
    private IWorldInfoContext? _dbContext;

    [OneTimeSetUp]
    public void Setup()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _dbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();

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
    }

    #region ValidateVatNumber

    [Test]
    public void VatNumber_WithValidUkraineNumber_ReturnsSuccess()
    {
        var vatDefinition = _dbContext!.VatNumberDefinitions.Get("UA")!;
        var validationResult = vatDefinition.Validate("0203654090", false)!;

        Assert.That(validationResult.Status, Is.EqualTo(VatValidationStatus.Valid));
    }

    [Test]
    public void VatNumber_WithValidUAPrefix_ReturnsSuccess()
    {
        var validationResult = _dbContext!.VatNumberDefinitions.Get("UA")!.Validate("UA0203654090", false)!;

        Assert.That(validationResult.Status, Is.EqualTo(VatValidationStatus.Valid));
    }

    [Test]
    public void VatNumber_WithInvalidUkraineValue_ReturnsFail()
    {
        var validationResult = _dbContext!.VatNumberDefinitions.Get("UA")!.Validate("123Test456", false)!;

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.Status, Is.EqualTo(VatValidationStatus.Invalid));
            Assert.That(validationResult.ValidationErrors, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void VatNumber_WithNotFoundSanMarinoNumber_ReturnsInvalid()
    {
        var validationResult = WorldInfo.VatNumberDefinitions
            .Get("SM")!
            .Validate("123456", false)!;

        Assert.That(validationResult.Status, Is.EqualTo(VatValidationStatus.Unverified));
    }

    #endregion ValidateVatNumber

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
    [TestCase("DK")]
    [TestCase("AT")]
    [TestCase("JP")]
    [TestCase("CN")]
    [TestCase("TR")]
    [TestCase("SE")]
    [TestCase("IL")]
    [TestCase("TH")]
    [TestCase("AE")]
    [TestCase("MY")]
    [TestCase("FI")]
    [TestCase("SG")]
    [TestCase("NO")]
    [TestCase("RU")]
    [TestCase("NZ")]
    [TestCase("SA")]
    [TestCase("PH")]
    [TestCase("ID")]
    [TestCase("HK")]
    public void VatNumber_TestNumberOfFailingVATPerCountry(string countryCode)
    {
        using var sr = new StreamReader($"{_testFilePath}/{countryCode}-VatNumbers.json");
        var testData = JsonSerializer.Deserialize<string[]>(sr.ReadToEnd());

        var failedVat = new List<string>();
        foreach (var vatNumber in testData!)
        {
            var validationResult = _dbContext.VatNumberDefinitions
                .Get(countryCode)!
                .Validate(vatNumber, false)!;

            if (validationResult.Status != VatValidationStatus.Valid)
            {
                failedVat.Add($"{vatNumber}:{string.Join(';', validationResult.ValidationErrors)}");
            }
        }

        Trace.WriteLine($"Failed VAT count: {failedVat.Count}/{testData.Length}");
        Trace.WriteLine("Failed VAT:");
    }

    [TestCase("44403198682", "FR", true)]
    [TestCase("157050817", "DE", true)]
    [TestCase("00386730462", "DE", false)]
    [TestCase("4410268272", "FR", false)]
    public void VatNumber_TestApiValidation_ViesApi(string vatNumber, string countryCode, bool isValid)
    {
        var validationResult = _dbContext!.VatNumberDefinitions.Get(countryCode)!.Validate(vatNumber)!;

        var status = isValid ? VatValidationStatus.Valid : VatValidationStatus.Invalid;
        Assert.Multiple(() =>
        {
            Assert.That(validationResult.Status, Is.EqualTo(status));
            Assert.That(((ViesVerificationResponse)validationResult.ApiVerificationData!).isValid, Is.EqualTo(isValid));
        });
    }

    #endregion PerCountry

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
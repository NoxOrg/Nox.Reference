using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Abstractions;
using Nox.Reference.Holidays;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Tests;

//TODO: transform into a command-like structure similarly to get data project
public class VatNumberTests
{
    private IWorldInfoContext? _dbContext;

    [OneTimeSetUp]
    public void Setup()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _dbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();
    }

    #region ValidateVatNumber

    [Test]
    public void VatNumber_WithValidUkraineNumber_ReturnsSuccess()
    {
        var vatDefinition = _dbContext!.VatNumberDefinitions.First(x => x.Country == "UA");
        var validationResult = vatDefinition.Validate("0203654090", "UA", false)!;

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.EqualTo(true));
            Assert.That(validationResult.IsVerified, Is.EqualTo(true));
        });
    }

    //[Test]
    //public void VatNumber_WithValidUAPrefix_ReturnsSuccess()
    //{
    //    var validationResult = _vatNumberService.ValidateVatNumber(new VatNumberInfo("UA", "UA0203654090"), false);

    //    Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(validationResult.ValidationResult.IsValid, Is.EqualTo(true));
    //        Assert.That(validationResult.IsVerified, Is.EqualTo(true));
    //    });
    //}

    //[Test]
    //public void VatNumber_WithInvalidUkraineValue_ReturnsFail()
    //{
    //    var validationResult = _vatNumberService.ValidateVatNumber(new VatNumberInfo("UA", "123Test456"), false);

    //    Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(validationResult.ValidationResult.IsValid, Is.EqualTo(false));
    //        Assert.That(validationResult.ValidationResult.ValidationErrors, Has.Count.EqualTo(1));
    //    });
    //}

    //[Test]
    //public void VatNumber_WithNotFoundSanMarinoNumber_ReturnsInvalid()
    //{
    //    var validationResult = _vatNumberService.ValidateVatNumber(new VatNumberInfo("SM", "123456"), false);

    //    Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(validationResult.ValidationResult.IsValid, Is.EqualTo(true));
    //        Assert.That(validationResult.IsVerified, Is.EqualTo(false));
    //    });
    //}

    //#endregion ValidateVatNumber

    //#region PerCountry

    //[Test]
    //[TestCase("UA")]
    //[TestCase("ZA")]
    //[TestCase("CO")]
    //[TestCase("CH")]
    //[TestCase("BR")]
    //[TestCase("GB")]
    //[TestCase("IT")]
    //[TestCase("FR")]
    //[TestCase("DE")]
    //[TestCase("ES")]
    //[TestCase("NL")]
    //[TestCase("MX")]
    //[TestCase("IN")]
    //[TestCase("CA")]
    //[TestCase("BE")]
    //[TestCase("AU")]
    //[TestCase("PL")]
    //[TestCase("PT")]
    //[TestCase("DK")]
    //[TestCase("AT")]
    //[TestCase("JP")]
    //[TestCase("CN")]
    //[TestCase("TR")]
    //[TestCase("SE")]
    //[TestCase("IL")]
    //[TestCase("TH")]
    //[TestCase("AE")]
    //[TestCase("MY")]
    //[TestCase("FI")]
    //[TestCase("SG")]
    //[TestCase("NO")]
    //[TestCase("RU")]
    //[TestCase("NZ")]
    //[TestCase("SA")]
    //[TestCase("PH")]
    //[TestCase("ID")]
    //[TestCase("HK")]
    //[Parallelizable(ParallelScope.All)]
    //public void VatNumber_TestNumberOfFailingVATPerCountry(string countryCode)
    //{
    //    using var sr = new StreamReader($"{_testFilePath}/{countryCode}-VatNumbers.json");
    //    var testData = JsonSerializer.Deserialize<string[]>(sr.ReadToEnd());

    //    var failedVat = new System.Collections.Generic.List<string>();
    //    foreach (var vatNumber in testData!)
    //    {
    //        var vatNumberInfo = _vatNumberService.ValidateVatNumber(new VatNumberInfo(countryCode, vatNumber), false);
    //        if (!vatNumberInfo.ValidationResult.IsValid ||
    //            !vatNumberInfo.IsVerified)
    //        {
    //            failedVat.Add($"{vatNumber}:{string.Join(';', vatNumberInfo.ValidationResult.ValidationErrors)}");
    //        }
    //    }

    //    Trace.WriteLine($"Failed VAT count: {failedVat.Count}/{testData.Length}");
    //    Trace.WriteLine("Failed VAT:");
    //    Trace.WriteLine(JsonSerializer.Serialize(failedVat, _jsonOptions));
    //}

    //[TestCase("44403198682", "FR", true)]
    //[TestCase("157050817", "DE", true)]
    //[TestCase("00386730462", "DE", false)]
    //[TestCase("4410268272", "FR", false)]
    //public void VatNumber_TestApiValidation_ViesApi(string vatNumber, string countryCode, bool isValid)
    //{
    //    var validationResult = _vatNumberService.ValidateVatNumber(new VatNumberInfo(countryCode, vatNumber));

    //    Trace.WriteLine(JsonSerializer.Serialize(validationResult, _jsonOptions));

    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(validationResult.ValidationResult.IsValid, Is.EqualTo(isValid));
    //        Assert.That(((ViesVerificationResponse)validationResult.ApiVerificationData!).isValid, Is.EqualTo(isValid));
    //        Assert.That(validationResult.IsVerified, Is.EqualTo(true));
    //    });
    //}

    #endregion ValidateVatNumber

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
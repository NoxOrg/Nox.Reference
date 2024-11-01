using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using static Nox.Reference.Common.NoxReferenceJsonSerializer;

namespace Nox.Reference.Data.World.Tests;

//TODO: transform into a command-like structure similarly to get data project
public class VatNumberTests
{
    private string _testFilePath = string.Empty;
    private IWorldInfoContext _worldDbContext = null!;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    [OneTimeSetUp]
    public void Setup()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        WorldDbContext.UseDatabaseConnectionString(DatabaseConstant.WorldDbPath);
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _worldDbContext = serviceProvider.GetRequiredService<IWorldInfoContext>();

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
        _testFilePath = DatabaseConstant.VatNumberTestDataPath;

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    #region ValidateVatNumber

    [Test]
    public void VatNumber_WithValidUkraineNumber_ReturnsSuccess()
    {
        var validationResult = _worldDbContext!
            .VatNumberDefinitions.Get("UA")!
            .Validate("0203654090", false)!;

        Trace.WriteLine(Serialize(validationResult));

        Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Valid));
    }

    [Test]
    public void VatNumber_WithValidUAPrefix_ReturnsSuccess()
    {
        var definition = _worldDbContext!.VatNumberDefinitions.Get("UA")!;
        var validationResult = definition.Validate("UA0203654090", false)!;

        var definitionInfo = definition.ToDto();

        Trace.WriteLine(Serialize(definitionInfo));
        Trace.WriteLine(Serialize(validationResult));

        Assert.That(definitionInfo, Is.Not.Null);
        Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Valid));
    }

    [Test]
    public void VatNumber_WithValidUAPrefixAndEnum_ReturnsSuccess()
    {
        var definition = _worldDbContext!.VatNumberDefinitions.Get(WorldCountries.Ukraine)!;
        var validationResult = definition.Validate("UA0203654090", false)!;

        Assert.That(definition.Country, Is.Not.Null);

        var definitionInfo = definition.ToDto();

        Trace.WriteLine(Serialize(definitionInfo));
        Trace.WriteLine(Serialize(validationResult));

        Assert.That(definitionInfo, Is.Not.Null);
        Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Valid));
    }

    [Test]
    public void VatNumber_WithValidUAPrefixAndEnumUsingCollection_ReturnsSuccess()
    {
        var validationResult = _worldDbContext!.VatNumberDefinitions.Validate(WorldCountries.Ukraine, "UA0203654090", false)!;

        Assert.That(validationResult.Country, Is.Not.Null);

        Trace.WriteLine(Serialize(validationResult));

        Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Valid));
    }

    [Test]
    public void VatNumber_WithInvalidUkraineValue_ReturnsFail()
    {
        var validationResult = _worldDbContext!.VatNumberDefinitions.Get("UA")!.Validate("123Test456", false)!;

        Trace.WriteLine(Serialize(validationResult));

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Invalid));
            Assert.That(validationResult.ValidationErrors, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void VatNumber_WithNotFoundSanMarinoNumber_ReturnsInvalid()
    {
        var validationResult = _worldDbContext.VatNumberDefinitions
            .Get("SM")!
            .Validate("123456", false)!;

        Trace.WriteLine(Serialize(validationResult));

        Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Unverified));
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
            var validationResult = _worldDbContext.VatNumberDefinitions
                .Get(countryCode)!
                .Validate(vatNumber, false)!;

            if (validationResult.Status != ValidationStatus.Valid)
            {
                failedVat.Add($"{vatNumber}:{string.Join(';', validationResult.ValidationErrors)}");
            }
        }

        Trace.WriteLine($"Failed VAT count: {failedVat.Count}/{testData.Length}");
        Trace.WriteLine("Failed VAT:");
        Trace.WriteLine(JsonSerializer.Serialize(failedVat, _jsonOptions));
    }

    [TestCase("44403198682", "FR", true)]
    [TestCase("129273398", "DE", true)]
    [TestCase("00386730462", "DE", false)]
    [TestCase("4410268272", "FR", false)]
    public void VatNumber_TestApiValidation_ViesApi(string vatNumber, string countryCode, bool isValid)
    {
        var validationResult = _worldDbContext!.VatNumberDefinitions.Get(countryCode)!.Validate(vatNumber, true)!;

        var status = isValid ? ValidationStatus.Valid : ValidationStatus.Invalid;

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.Status, Is.EqualTo(status));
            Assert.That(((ViesVerificationResponse)validationResult.ApiVerificationData!).isValid, Is.EqualTo(isValid));
        });
    }

    #endregion PerCountry

    [Test]
    public void VatNumber_Automapper()
    {
        var validationDefinition = _worldDbContext.VatNumberDefinitions.Get("DE")!;

        var validationDefinitionInfo = validationDefinition.ToDto();

        Trace.WriteLine(Serialize(validationDefinitionInfo));

        Assert.That(validationDefinitionInfo, Is.Not.Null);
    }

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using static Nox.Reference.Common.NoxReferenceJsonSerializer;

namespace Nox.Reference.Data.World.Tests;

//TODO: transform into a command-like structure similarly to get data project
public class TaxNumberTests
{
    private string _testFilePath = string.Empty;
    private IWorldInfoContext _dbContext = null!;

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
        // TODO: come up with different test data
        _testFilePath = DatabaseConstant.TaxNumberTestDataPath;

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    #region ValidateTaxNumber

    [Test]
    public void TaxNumber_WithValidUkraineNumber_ReturnsSuccess()
    {
        var validationResult = _dbContext!
            .TaxNumberDefinitions.Get("UA")!
            .Validate("0203654090", false)!;

        Trace.WriteLine(Serialize(validationResult));

        Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Valid));
    }

    [Test]
    public void TaxNumber_WithValidUAPrefix_ReturnsSuccess()
    {
        var definition = _dbContext!.TaxNumberDefinitions.Get("UA")!;
        var validationResult = definition.Validate("UA0203654090", false)!;

        Assert.That(definition.Country, Is.Not.Null);

        var mappedDefinition = definition.ToDto();

        Trace.WriteLine(Serialize(mappedDefinition));
        Trace.WriteLine(Serialize(validationResult));

        Assert.That(mappedDefinition, Is.Not.Null);
        Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Valid));
    }

    [Test]
    public void TaxNumber_WithValidUAPrefixAndEnum_ReturnsSuccess()
    {
        var definition = _dbContext!.TaxNumberDefinitions.Get(WorldCountries.Ukraine)!;
        var validationResult = definition.Validate("UA0203654090", false)!;

        Assert.That(definition.Country, Is.Not.Null);

        var mappedDefinition = definition.ToDto();

        Trace.WriteLine(Serialize(mappedDefinition));
        Trace.WriteLine(Serialize(validationResult));

        Assert.That(mappedDefinition, Is.Not.Null);
        Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Valid));
    }

    [Test]
    public void TaxNumber_WithValidUAPrefixAndEnumUsingCollection_ReturnsSuccess()
    {
        var validationResult = _dbContext!.TaxNumberDefinitions.Validate(WorldCountries.Ukraine, "UA0203654090", false)!;

        Assert.That(validationResult.Country, Is.Not.Null);

        Trace.WriteLine(Serialize(validationResult));

        Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Valid));
    }

    [Test]
    public void TaxNumber_WithInvalidUkraineValue_ReturnsFail()
    {
        var validationResult = _dbContext!.TaxNumberDefinitions.Get("UA")!.Validate("123Test456", false)!;

        Trace.WriteLine(Serialize(validationResult));

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Invalid));
            Assert.That(validationResult.ValidationErrors, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void TaxNumber_WithNotFoundSanMarinoNumber_ReturnsInvalid()
    {
        var validationResult = Reference.World.TaxNumberDefinitions
            .Get("SM")!
            .Validate("123456", false)!;

        Trace.WriteLine(Serialize(validationResult));

        Assert.That(validationResult.Status, Is.EqualTo(ValidationStatus.Unverified));
    }

    #endregion ValidateTaxNumber

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
    [TestCase("TR")]
    [TestCase("SE")]
    [TestCase("IL")]
    [TestCase("TH")]
    [TestCase("AE")]
    [TestCase("MY")]
    [TestCase("FI")]
    [TestCase("NO")]
    [TestCase("RU")]
    [TestCase("NZ")]
    [TestCase("PH")]
    [TestCase("ID")]
    public void TaxNumber_TestNumberOfFailingVATPerCountry(string countryCode)
    {
        using var sr = new StreamReader($"{_testFilePath}/{countryCode}-VatNumbers.json");
        var testData = JsonSerializer.Deserialize<string[]>(sr.ReadToEnd());

        var failedVat = new List<string>();
        foreach (var vatNumber in testData!)
        {
            var validationResult = _dbContext.TaxNumberDefinitions
                .Get(countryCode)!
                .Validate(vatNumber, false)!;

            if (validationResult.Status != ValidationStatus.Valid)
            {
                failedVat.Add($"{vatNumber}:{string.Join(';', validationResult.ValidationErrors)}");
            }
        }

        Trace.WriteLine($"Failed TAX count: {failedVat.Count}/{testData.Length}");
        Trace.WriteLine("Failed TAX:");
        Trace.WriteLine(JsonSerializer.Serialize(failedVat, _jsonOptions));
    }

    #endregion PerCountry

    [Test]
    public void TaxNumber_Automapper()
    {
        var validationDefinition = Reference.World.TaxNumberDefinitions.Get("UA")!;

        var mappedResult = validationDefinition.ToDto();

        Trace.WriteLine(Serialize(mappedResult));

        Assert.That(mappedResult, Is.Not.Null);
    }

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
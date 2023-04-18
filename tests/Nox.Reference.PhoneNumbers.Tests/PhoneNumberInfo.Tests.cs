using System.Diagnostics;
using System.Text.Json;

namespace Nox.Reference.PhoneNumbers.Tests;

public class PhoneNumberInfoTests
{
    private readonly JsonSerializerOptions _jsonOptions = new() 
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    [OneTimeSetUp]
    public void Setup()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [Test]
    public void PhoneNumberInfo_WithKnownSouthAfricanMobileNumber_ReturnsValidInfo()
    {
        IPhoneNumberService phoneNumberService = new PhoneNumberService();

        IPhoneNumberInfo info = phoneNumberService.GetPhoneNumberInfo("833770694", "ZA");

        Trace.WriteLine(JsonSerializer.Serialize(info, _jsonOptions));

        Assert.Multiple(() =>
        {
            Assert.That(info.InputNumber, Is.EqualTo("833770694"));
            Assert.That(info.FormattedNumber, Is.EqualTo("+27833770694"));
            Assert.That(info.FormattedNumberNational, Is.EqualTo("083 377 0694"));
            Assert.That(info.FormattedNumberRfc3966, Is.EqualTo("tel:+27-83-377-0694"));
            Assert.That(info.IsValid, Is.EqualTo(true));
            Assert.That(info.IsMobile, Is.EqualTo(true));
            Assert.That(info.NumberType, Is.EqualTo("MOBILE"));
            Assert.That(info.RegionCode, Is.EqualTo("ZA"));
            Assert.That(info.RegionName, Is.EqualTo("South Africa"));
            Assert.That(info.CarrierName, Is.EqualTo("MTN"));
        });
        
    }

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
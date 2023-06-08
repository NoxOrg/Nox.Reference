using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Models;
using Nox.Reference.PhoneNumbers;
using System.Diagnostics;
using System.Linq;

namespace Nox.Reference.Data.World.Tests;

public class PhoneNumberInfoTests
{
    private IPhoneNumberService _phoneNumberService = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        WorldDbContext.UseDatabasePath(DatabaseConstant.WorldDbPath);
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _phoneNumberService = serviceProvider.GetRequiredService<IPhoneNumberService>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [Test]
    public void PhoneNumberInfo_WithKnownSouthAfricanMobileNumber_ReturnsValidInfo()
    {
        var info = _phoneNumberService.GetPhoneNumberInfo("833770694", "ZA");

        Trace.WriteLine(NoxReferenceJsonSerializer.Serialize(info));

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

    [Test]
    public void GetPhoneCarriers_StaticWithMap_ReturnsResult()
    {
        var carrierPhoneNumbers = World.PhoneNumbers.PhoneCarriers.First(x => x.Name == "Kyivstar");

        var carrierPhoneNumberInfos = carrierPhoneNumbers.ToDto<PhoneCarrierInfo>();

        Assert.Multiple(() =>
        {
            Assert.That(carrierPhoneNumberInfos, Is.Not.Null);
            Assert.That(carrierPhoneNumberInfos.Name, Is.EqualTo("Kyivstar"));
            Assert.That(carrierPhoneNumberInfos.PhoneNumbers.Count, Is.EqualTo(3));
        });
    }

    [TearDown]
    public void EndTest()
    {
        Trace.Flush();
    }
}
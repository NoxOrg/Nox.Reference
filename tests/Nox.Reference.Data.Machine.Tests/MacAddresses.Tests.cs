using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.Machine.Tests;
using System.Diagnostics;

namespace Nox.Reference.Data.Tests;

public class MacAddressesTests
{
    private IMachineInfoContext _macAddressContext;

    [OneTimeSetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddMachineContext();
        MachineDbContext.UseDatabaseConnectionString(DatabaseConstant.MachineDbConnectionString);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _macAddressContext = serviceProvider.GetRequiredService<IMachineInfoContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [TestCase("00:16:F6:11:22:33", "0016F6", "Nevion")]
    [TestCase("00-16-F6-11-22-33", "0016F6", "Nevion")]
    [TestCase("00 16 F6 11 22 33", "0016F6", "Nevion")]
    [TestCase("0016F6112233", "0016F6", "Nevion")]
    public void GetVendorMacAddress_ValidMacAddressString_ReturnsValidInfo(
        string input,
        string expectedPrefix,
        string expectedOrganizationName)
    {
        var info = _macAddressContext.MacAddresses.Get(input)!;
        Assert.Multiple(() =>
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(expectedPrefix, Is.EqualTo(info.Id));
        });
        var mappedInfo = info.ToDto();

        Assert.That(mappedInfo, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(mappedInfo.MacPrefix, Is.EqualTo(expectedPrefix));
            Assert.That(mappedInfo.OrganizationName, Is.EqualTo(expectedOrganizationName));
        });
    }

    [TestCase("00:16:F6:11:22:33", "0016F6", "Nevion")]
    [TestCase("00-16-F6-11-22-33", "0016F6", "Nevion")]
    public void GetVendorMacAddress_StaticCall_ReturnsValidInfo(
        string input,
        string expectedPrefix,
        string expectedOrganizationName)
    {
        var info = Reference.Machine.MacAddresses.Get(input)!;

        var mappedInfo = info.ToDto();

        Assert.That(mappedInfo, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(mappedInfo.MacPrefix, Is.EqualTo(expectedPrefix));
            Assert.That(mappedInfo.OrganizationName, Is.EqualTo(expectedOrganizationName));
        });
    }

    [Test]
    public void GetVendorMacAddress_ConvertToDto_ReturnsSuccess()
    {
        MacAddress macAddress = Reference.Machine.MacAddresses.Get("00:16:F6:11:22:33")!;

        MacAddressInfo macAddressInfo = macAddress.ToDto();
        Assert.Multiple(() =>
        {
            Assert.That(macAddressInfo, Is.Not.Null);
            Assert.That(macAddressInfo.MacPrefix, Is.EqualTo("0016F6"));
        });
    }
}
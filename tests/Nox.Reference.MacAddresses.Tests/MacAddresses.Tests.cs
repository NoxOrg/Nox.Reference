using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.MacAddresses.Tests;

public class MacAddressesTests
{
    private IMacAddressService _macAddressService;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddNoxMacAddresses();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _macAddressService = serviceProvider.GetRequiredService<IMacAddressService>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [Test]
    public void GetMacAddresses_AllOrganiztion_ReturnsValidInfo()
    {
        var addresses = _macAddressService.GetMacAddresses();

        Assert.That(addresses, Is.Not.Empty);
    }

    [Test]
    public void FindMacAddressesByVendor_OrganiztionPart_ReturnsValidInfo()
    {
        var addresses = _macAddressService.LookupMacAddressInfoByOrganiztion("xerox");

        Assert.That(addresses, Is.Not.Empty);
        Assert.That(addresses.Count(), Is.GreaterThan(1));
    }

    [Test]
    public void FindMacAddressesByVendor_NoAnyMatches_ReturnsEmpty()
    {
        var addresses = _macAddressService.LookupMacAddressInfoByOrganiztion("1xerox1");

        Assert.That(addresses, Is.Empty);
        Assert.That(addresses.Count(), Is.EqualTo(0));
    }

    [Test]
    public void GetVendorMacAddress_CertainOrganization_ReturnsValidInfo()
    {
        var info = _macAddressService.GetMacAddressInfo("Nevion");

        Assert.That(info, Is.Not.Null);
        Assert.That(info.Assignment, Is.EqualTo("0016F6"));
    }

    [Test]
    public void GetVendorMacAddress_OrganizationNotExist_ReturnsEmpty()
    {
        var info = _macAddressService.GetMacAddressInfo("1Cisco1");

        Assert.That(info, Is.Null);
    }
}
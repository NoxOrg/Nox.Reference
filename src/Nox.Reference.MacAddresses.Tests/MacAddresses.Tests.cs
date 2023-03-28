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
        serviceCollection.AddMacAddresses();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _macAddressService = serviceProvider.GetRequiredService<IMacAddressService>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [Test]
    public void GetMacAddresses_AllVendors_ReturnsValidInfo()
    {
        var addresses = _macAddressService.GetMacAddresses();

        Assert.That(addresses, Is.Not.Empty);
    }

    [Test]
    public void FindMacAddressesByVendor_VendorPart_ReturnsValidInfo()
    {
        var addresses = _macAddressService.FindMacAddressByVendor("xerox");

        Assert.That(addresses, Is.Not.Empty);
        Assert.That(addresses.Count(), Is.GreaterThan(1));
    }

    [Test]
    public void GetVendorMacAddress_CertainVendor_ReturnsValidInfo()
    {
        var info = _macAddressService.GetVendorMacAddress("Cisco");

        Assert.That(info, Is.Not.Null);
        Assert.That(info.Address, Is.EqualTo("00000C"));
    }
}
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.MacAddress.DataContext;
using Nox.Reference.MacAddresses.DataContext;

namespace Nox.Reference.MacAddresses.Tests;

public class MacAddressesTests
{
    private IMacAddressContext _macAddressContext;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddMacAddressDbContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _macAddressContext = serviceProvider.GetRequiredService<IMacAddressContext>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [TestCase("00:16:F6:11:22:33", "0016F6", "Nevion")]
    [TestCase("00-16-F6-11-22-33", "0016F6", "Nevion")]
    public void GetVendorMacAddress_ValidMacAddressString_ReturnsValidInfo(
        string input,
        string expectedPrefix,
        string expectedOrganizationName)
    {
        var info = _macAddressContext.MacAddresses.Get(input);

        Assert.That(info, Is.Not.Null);
        Assert.That(info.Id, Is.EqualTo(expectedPrefix));
        Assert.That(info.MacPrefix, Is.EqualTo(expectedPrefix));
        Assert.That(info.OrganizationName, Is.EqualTo(expectedOrganizationName));
    }

    [TestCase("V0:16:F6:11:22:33")]
    [TestCase("0016-F6-11-22-33")]
    [TestCase("")]
    [TestCase(null)]
    public void GetVendorMacAddress_InvalidMacAddressString_ShouldThrow(string input)
    {
        Assert.Throws<ArgumentException>(() => _macAddressContext.MacAddresses.FirstOrDefault(x => x.MacPrefix == input));
    }
}
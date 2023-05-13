using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.Machine;

namespace Nox.Reference.Data.Tests;

public class MacAddressesTests
{
    private IMachineInfoContext _macAddressContext;

    [OneTimeSetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddMachineContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _macAddressContext = serviceProvider.GetRequiredService<IMachineInfoContext>();

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
}
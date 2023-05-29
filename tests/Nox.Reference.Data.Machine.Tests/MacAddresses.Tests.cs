using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.Machine;
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
        MachineDbContext.UseDatabasePath(DatabaseConstant.MachineDbPath);

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

        var mappedInfo = Machine.Machine.Mapper.Map<MacAddressInfo>(info);

        Assert.That(mappedInfo, Is.Not.Null);
        Assert.That(mappedInfo.MacPrefix, Is.EqualTo(expectedPrefix));
        Assert.That(mappedInfo.OrganizationName, Is.EqualTo(expectedOrganizationName));
    }

    [TestCase("00:16:F6:11:22:33", "0016F6", "Nevion")]
    [TestCase("00-16-F6-11-22-33", "0016F6", "Nevion")]
    public void GetVendorMacAddress_StaticCall_ReturnsValidInfo(
        string input,
        string expectedPrefix,
        string expectedOrganizationName)
    {
        var info = Machine.Machine.MacAddresses.Get(input);

        var mappedInfo = Machine.Machine.Mapper.Map<MacAddressInfo>(info);

        Assert.That(mappedInfo, Is.Not.Null);
        Assert.That(mappedInfo.MacPrefix, Is.EqualTo(expectedPrefix));
        Assert.That(mappedInfo.OrganizationName, Is.EqualTo(expectedOrganizationName));
    }
}
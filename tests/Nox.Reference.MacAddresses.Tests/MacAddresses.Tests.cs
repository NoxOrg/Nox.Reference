using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.MacAddresses.Tests;

public class MacAddressesTests
{
    private IMacAddressService _macAddressService;

    [SetUp]
    public void Setup()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new List<KeyValuePair<string, string?>>()
            {
                new KeyValuePair<string, string?>( "ConnectionStrings:noxreferencesConnection", @"Data Source=..\..\..\..\..\data\noxreferences.db")
            })
            .Build();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddNoxMacAddresses(configuration);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _macAddressService = serviceProvider.GetRequiredService<IMacAddressService>();

        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [TestCase("00:16:F6:11:22:33", "0016F6", "Nevion")]
    [TestCase("00-16-F6-11-22-33", "0016F6", "Nevion")]
    public void GetVendorMacAddress_ValidMacAddressString_ReturnsValidInfo(
        string input,
        string expectedPrefix,
        string expectedOrganizationName)
    {
        var info = _macAddressService.GetMacAddressInfo(input);

        Assert.That(info, Is.Not.Null);
        Assert.That(info.MacPrefix, Is.EqualTo(expectedPrefix));
        Assert.That(info.OrganizationName, Is.EqualTo(expectedOrganizationName));
    }

    [TestCase("V0:16:F6:11:22:33")]
    [TestCase("0016-F6-11-22-33")]
    [TestCase("")]
    [TestCase(null)]
    public void GetVendorMacAddress_InvalidMacAddressString_ShouldThrow(string input)
    {
        Assert.Throws<ArgumentException>(() => _macAddressService.GetMacAddressInfo(input));
    }
}
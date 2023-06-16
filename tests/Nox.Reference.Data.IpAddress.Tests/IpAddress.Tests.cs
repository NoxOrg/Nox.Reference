using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Nox.Reference.Data.IpAddress.Tests
{
    public class IpAddressTests
    {
        private IpAddressService _ipAddressService;

        [OneTimeSetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddIpAddressContext();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IpAddressDbContext.UseDatabaseConnectionString(configuration.GetConnectionString(ConfigurationConstants.IpAddressConnectionStringName)!);
            _ipAddressService = serviceProvider.GetRequiredService<IpAddressService>();

            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [TestCase("81.90.224.0", "UA")]
        [TestCase("45.12.25.125", "UA")]
        [TestCase("103.190.102.11", "CH")]
        [TestCase("168.235.255.7", "GB")]
        [TestCase("142.251.32.46", "US")]
        [TestCase("142.251.32.46", "US")]
        [TestCase("149.126.4.31", "CH")]
        [TestCase("2a01:ab20:0:4::31", "CH")]
        [TestCase("2001:470:71:21::", "UA")]
        [TestCase("2e00::", "DE")]
        [TestCase("2607:f8b0:3d00::", "US")]
        [TestCase("2001:4860:4860::", "US")]
        public void IpAddress_DetermineCountryByIpCode_Success(string ipAddress, string expectedCountryCode)
        {
            var countryCode = _ipAddressService.GetCountryByIp(ipAddress);

            Assert.That(countryCode, Is.EqualTo(expectedCountryCode));
        }
    }
}
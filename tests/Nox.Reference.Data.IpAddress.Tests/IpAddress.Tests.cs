using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Nox.Reference.Data.IpAddress.Tests
{
    public class IpAddressTests
    {
        private IIpAddressService _ipAddressService;

        [OneTimeSetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddIpAddressContext();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _ipAddressService = serviceProvider.GetRequiredService<IIpAddressService>();

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
            var result = _ipAddressService.GetCountryByIp(ipAddress);

            Assert.Multiple(() =>
            {
                Assert.That(result.Kind, Is.EqualTo(IpSearchResultKind.Success));
                Assert.That(expectedCountryCode, Is.EqualTo(result.CountryCode));
            });
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
        public void IpAddressStatic_DetermineCountryByIpCode_Success(string ipAddress, string expectedCountryCode)
        {
            var result = IpAddressContext.GetCountryByIp(ipAddress);

            Assert.Multiple(() =>
            {
                Assert.That(result.Kind, Is.EqualTo(IpSearchResultKind.Success));
                Assert.That(expectedCountryCode, Is.EqualTo(result.CountryCode));
            });
        }

        [TestCase("0.0.0.0")]
        [TestCase("192.168.1.1")]
        public void IpAddress_DetermineCountryByIpCode_NotFound(string ipAddress)
        {
            var result = IpAddressContext.GetCountryByIp(ipAddress);

            Assert.That(result.Kind, Is.EqualTo(IpSearchResultKind.NotFound));
        }

        [TestCase("81.90.224.023232")]
        [TestCase("4a5.12.25.125")]
        [TestCase("26aa07:f8b0:3d00::3")]
        [TestCase("[2001:db8:0:1]:80")]
        public void IpAddress_CountryByIpCodeWithIncorrectData_IncorrectInputResult(string ipAddress)
        {
            var result = _ipAddressService.GetCountryByIp(ipAddress);

            Assert.That(result.Kind, Is.EqualTo(IpSearchResultKind.IncorrectInput));
        }
    }
}
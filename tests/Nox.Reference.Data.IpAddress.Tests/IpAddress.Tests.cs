using System.Diagnostics;
using System.Net;
using System.Numerics;
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
            IpAddressDbContext.UseDatabaseConnectionString(DatabaseConstant.IpAddressDbPath);
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
        public void IpAddress_DetermineCountryByIPString_Success(string ipAddress, string expectedCountryCode)
        {
            var result = _ipAddressService.GetCountryByIPAddress(ipAddress);

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
        public void IpAddressStatic_DetermineCountryCodeByIPString_Success(string ipAddress, string expectedCountryCode)
        {
            var result = Reference.IpAddress.GetCountryByIPAddress(ipAddress);

            Assert.Multiple(() =>
            {
                Assert.That(result.Kind, Is.EqualTo(IpSearchResultKind.Success));
                Assert.That(expectedCountryCode, Is.EqualTo(result.CountryCode));
            });
        }

        [TestCase("103.190.102.11", "CH")]
        [TestCase("168.235.255.7", "GB")]
        [TestCase("2607:f8b0:3d00::", "US")]
        [TestCase("2001:4860:4860::", "US")]
        public void IpAddress_DetermineCountryCodeByIPObject_Success(string ipAddress, string expectedCountryCode)
        {
            var ipAddressObj = IPAddress.Parse(ipAddress);
            var result = _ipAddressService.GetCountryByIPAddress(ipAddressObj);

            Assert.Multiple(() =>
            {
                Assert.That(result.Kind, Is.EqualTo(IpSearchResultKind.Success));
                Assert.That(expectedCountryCode, Is.EqualTo(result.CountryCode));
            });
        }

        [TestCase("3425705984", "CH")]
        [TestCase("3427484159", "GB")]
        [TestCase("61144487806106130153575124772895850496", "DE")]
        [TestCase("63802943797675961899382738893456539647", "DE")]
        public void IpAddress_DetermineCountryCodeByIPNumber_Success(string ipAddressNumberString, string expectedCountryCode)
        {
            var ipAddressNumber = BigInteger.Parse(ipAddressNumberString);
            var result = _ipAddressService.GetCountryByIPAddress(ipAddressNumber);

            Assert.Multiple(() =>
            {
                Assert.That(result.Kind, Is.EqualTo(IpSearchResultKind.Success));
                Assert.That(expectedCountryCode, Is.EqualTo(result.CountryCode));
            });
        }

        [TestCase("0.0.0.0")]
        [TestCase("192.168.1.1")]
        public void IpAddress_DetermineCountryByIpString_NotFound(string ipAddress)
        {
            var result = _ipAddressService.GetCountryByIPAddress(ipAddress);

            Assert.That(result.Kind, Is.EqualTo(IpSearchResultKind.NotFound));
        }

        [TestCase("81.90.224.023232")]
        [TestCase("4a5.12.25.125")]
        [TestCase("26aa07:f8b0:3d00::3")]
        [TestCase("[2001:db8:0:1]:80")]
        public void IpAddress_CountryByIpCodeWithIncorrectData_IncorrectInputResult(string ipAddress)
        {
            var result = _ipAddressService.GetCountryByIPAddress(ipAddress);

            Assert.That(result.Kind, Is.EqualTo(IpSearchResultKind.IncorrectInput));
        }
    }
}
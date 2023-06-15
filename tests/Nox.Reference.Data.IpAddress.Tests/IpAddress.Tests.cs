using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using NUnit.Framework;

namespace Nox.Reference.Data.IpAddress.Tests
{
    public class Tests
    {
        private IIpAddressInfoContext _dataContext;

        [OneTimeSetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddIpAddressContext();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IpAddressDbContext.UseDatabaseConnectionString(configuration.GetConnectionString(ConfigurationConstants.IpAddressConnectionStringName)!);
            _dataContext = serviceProvider.GetRequiredService<IIpAddressInfoContext>();

            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [Test]
        public void Test1()
        {
            var ipAddressInput = "1.0.0.0";
            var ipAddress = System.Net.IPAddress.Parse(ipAddressInput);

            var vvv = _dataContext.IpAddresses.First();

            //var str = Encoding.ASCII.GetString(vvv.StartAddress);
            //var ct = System.Net.IPAddress.Parse(str);

            //var ipAddressInput1 = "16777216";
            //var ipAddress1 = System.Net.IPAddress.Parse(ipAddressInput1);
            ////var vvv = _dataContext.IpAddresses.First();

            //var vvv1 = _dataContext.IpAddresses.Skip(480331).First();
            //// var str1 = Encoding.ASCII.GetString(vvv1.StartAddress);
            //var ipAddress3 = new System.Net.IPAddress(vvv1.StartAddress);
            //new UInt128()
            //var ct = new System.Net.IPAddress(vvv.StartAddress);

            //var ct1 = new System.Net.IPAddress(vvv.EndAddress);
            //var pp = ct.ToString();
            //var pp1 = ct1.ToString();

            //var addressBytes = ipAddress.GetAddressBytes();
            //addressBytes.CompareTo()
            //_dataContext.IpAddresses.First(x=>x.StartAddress.coma > = )
            //Assert.Pass();
        }
    }
}
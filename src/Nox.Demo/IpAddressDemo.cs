using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference;

namespace Nox.Demo
{
    internal static class IpAddressDemo
    {
        private static IIpAddressService _ipAddressService;

        static IpAddressDemo()
        {
            var services = new ServiceCollection();
            services.AddIpAddressContext();

            _ipAddressService = services.BuildServiceProvider().GetRequiredService<IIpAddressService>();
        }

        public static void ShowDemo()
        {
            var ipAddressExamples = new List<(string ip, string country)>
            {
                ("81.90.224.0", "UA"),
                ("45.12.25.125", "UA"),
                ("103.190.102.11", "CH"),
                ("168.235.255.7", "GB"),
                ("142.251.32.46", "US"),
                ("142.251.32.46", "US"),
                ("149.126.4.31", "CH"),
                ("2a01:ab20:0:4::31", "CH"),
                ("2001:470:71:21::", "UA"),
                ("2e00::", "DE"),
                ("2607:f8b0:3d00::", "US"),
                ("2001:4860:4860::", "US")
            };

            //Use DI
            Console.WriteLine("Get IpAddress info using DI");
            foreach (var (ip, country) in ipAddressExamples)
            {
                IpSearchResult result = _ipAddressService.GetCountryByIp(ip);

                Console.WriteLine($"Result = {result.Kind} ... Country Code - Expected: {country}. Actual:  {result.CountryCode} ");
            }

            //Static use
            Console.WriteLine("Get IpAddress info using static approach");
            foreach (var entry in ipAddressExamples)
            {
                IpSearchResult result = IpAddressContext.GetCountryByIp(entry.ip);

                Console.WriteLine($"Result = {result.Kind} . Country code = {result.CountryCode} ");
            }
        }
    }
}
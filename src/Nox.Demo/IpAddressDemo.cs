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
                ("149.126.4.31", "CH"),
                ("81.90.224.0", "UA"),
                ("142.251.32.46", "US"),
                ("142.251.32.46", "US"),
                ("2a01:ab20:0:4::31", "CH"),
                ("2001:470:71:21::", "UA"),
                ("2e00::", "DE")
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
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses.Models
{
    public class MacAddressInfo : IMacAddressInfo
    {
        public MacAddressInfo(string address, string vendor)
        {
            Address = address;
            Vendor = vendor;
        }

        public string Address { get; init; }
        public string Vendor { get; init; }
    }
}
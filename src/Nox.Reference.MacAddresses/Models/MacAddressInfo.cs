using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses.Models
{
    internal class MacAddressInfo : IMacAddressInfo
    {
        public string Registry { get; init; }
        public string Assignment { get; init; }
        public string OrganizationName { get; init; }
        public string OrganizationAddress { get; init; }
    }
}
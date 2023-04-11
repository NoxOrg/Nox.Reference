using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.Data.Models
{
    internal class MacAddressInfo : IMacAddressInfo
    {
        public string IEEERegistry { get; private set; }
        public string Id { get; private set; }
        public string MacPrefix { get; private set; }
        public string OrganizationName { get; private set; }
        public string OrganizationAddress { get; private set; }
    }
}
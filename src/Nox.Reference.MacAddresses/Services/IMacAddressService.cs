using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses
{
    public interface IMacAddressService
    {
        IReadOnlySet<IMacAddressInfo> GetMacAddresses();
    }
}
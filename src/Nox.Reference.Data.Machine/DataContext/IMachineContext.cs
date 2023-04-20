using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.Data.Machine;

public interface IMachineContext
{
    IQueryable<IMacAddressInfo> MacAddresses { get; }
}
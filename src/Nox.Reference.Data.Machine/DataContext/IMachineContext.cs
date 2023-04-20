using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.Machine;

public interface IMachineContext
{
    IQueryable<IMacAddressInfo> MacAddresses { get; }
}
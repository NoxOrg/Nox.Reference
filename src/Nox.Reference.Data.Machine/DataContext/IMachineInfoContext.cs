using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.Machine;

public interface IMachineInfoContext
{
    IQueryable<IMacAddressInfo> MacAddresses { get; }
}
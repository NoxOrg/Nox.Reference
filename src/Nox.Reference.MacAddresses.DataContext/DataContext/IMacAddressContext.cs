using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses.DataContext;

public interface IMacAddressContext
{
    IQueryable<IMacAddressInfo> MacAddresses { get; }
}
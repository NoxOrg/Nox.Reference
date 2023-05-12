namespace Nox.Reference.Data.Machine;

public interface IMachineContext
{
    IQueryable<MacAddress> MacAddresses { get; }
}
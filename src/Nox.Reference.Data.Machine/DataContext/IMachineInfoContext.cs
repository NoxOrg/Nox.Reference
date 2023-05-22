namespace Nox.Reference.Data.Machine;

public interface IMachineInfoContext
{
    IQueryable<MacAddress> MacAddresses { get; }
}
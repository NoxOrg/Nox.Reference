namespace Nox.Reference;

public interface IMachineInfoContext
{
    IQueryable<MacAddress> MacAddresses { get; }
}
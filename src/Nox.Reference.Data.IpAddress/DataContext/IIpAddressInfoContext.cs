namespace Nox.Reference.Data.IpAddress;

public interface IIpAddressInfoContext
{
    IQueryable<IpAddress> IpAddresses { get; }
}


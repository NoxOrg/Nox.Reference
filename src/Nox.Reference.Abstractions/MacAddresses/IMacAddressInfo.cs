namespace Nox.Reference.Abstractions.MacAddresses;

public interface IMacAddressInfo
{
    string Registry { get; }
    string Assignment { get; }
    string OrganizationName { get; }
    string OrganizationAddress { get; }
}
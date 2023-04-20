namespace Nox.Reference.Abstractions;

public interface IMacAddressInfo
{
    string IEEERegistry { get; }
    string Id { get; }
    string MacPrefix { get; }
    string OrganizationName { get; }
    string OrganizationAddress { get; }
}
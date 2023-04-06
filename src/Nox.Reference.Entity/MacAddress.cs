using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.Entity;

public class MacAddress : IMacAddressInfo, INoxReferenceEntity
{
#nullable disable warnings
    public string Id { get; init; }

    public string MacPrefix { get; init; }

    public string IEEERegistry { get; init; }

    public string OrganizationName { get; init; }

    public string OrganizationAddress { get; init; }

#nullable enable warnings
}
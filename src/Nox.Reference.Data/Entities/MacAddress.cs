using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data.Seeds;

namespace Nox.Reference.Data.Entities;

internal class MacAddress : IMacAddressInfo, INoxReferenceEntity
{
    public string Id { get; private set; }
    public string IEEERegistry { get; private set; }
    public string MacPrefix { get; private set; }
    public string OrganizationName { get; private set; }
    public string OrganizationAddress { get; private set; }
}
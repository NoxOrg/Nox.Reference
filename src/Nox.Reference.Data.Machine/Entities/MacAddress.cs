using Nox.Reference.Data;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.Machine;

internal class MacAddress : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string IEEERegistry { get; private set; } = string.Empty;
    public string MacPrefix { get; private set; } = string.Empty;
    public string OrganizationName { get; private set; } = string.Empty;
    public string OrganizationAddress { get; private set; } = string.Empty;
}
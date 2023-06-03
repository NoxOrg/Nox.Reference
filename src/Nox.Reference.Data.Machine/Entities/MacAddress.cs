using Nox.Reference.Data.Common;

namespace Nox.Reference.Data;

// TODO: Add docs
public class MacAddress : INoxReferenceEntity
{
    // TODO: hide ID here and in world so internal entity framework
    // id is not misleading for customers
    public int Id { get; private set; }
    public string IEEERegistry { get; private set; } = string.Empty;
    public string MacPrefix { get; private set; } = string.Empty;
    public string OrganizationName { get; private set; } = string.Empty;
    public string OrganizationAddress { get; private set; } = string.Empty;
}
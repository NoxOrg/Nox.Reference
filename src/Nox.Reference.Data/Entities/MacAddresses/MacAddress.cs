namespace Nox.Reference.Data;

internal class MacAddress : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string IEEERegistry { get; private set; }
    public string MacPrefix { get; private set; }
    public string OrganizationName { get; private set; }
    public string OrganizationAddress { get; private set; }
}
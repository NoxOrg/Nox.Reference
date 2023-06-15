using CsvHelper.Configuration.Attributes;

namespace Nox.Reference;

public class MacAddressInfo
{
    [Name("Assignment")]
    public string Id { get; init; } = string.Empty;

    [Name("Assignment")]
    public string MacPrefix { get; init; } = string.Empty;

    [Name("Registry")]
    public string IEEERegistry { get; init; } = string.Empty;

    [Name("Organization Name")]
    public string OrganizationName { get; init; } = string.Empty;

    [Name("Organization Address")]
    public string OrganizationAddress { get; init; } = string.Empty;
}
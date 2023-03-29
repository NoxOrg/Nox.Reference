using CsvHelper.Configuration.Attributes;
using Nox.Reference.Abstractions.MacAddresses;

internal record MacAddressInfo : IMacAddressInfo
{
    public string Registry { get; init; }
    public string Assignment { get; init; }

    [Name("Organization Name")]
    public string OrganizationName { get; init; }
    [Name("Organization Address")]
    public string OrganizationAddress { get; init; }
}
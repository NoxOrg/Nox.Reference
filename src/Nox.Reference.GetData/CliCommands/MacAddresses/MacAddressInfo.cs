using CsvHelper.Configuration.Attributes;
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.GetData.CliCommands;

internal record MacAddressInfo : IMacAddressInfo
{
#nullable disable warnings
    public string Registry { get; init; }
    public string Assignment { get; init; }

    [Name("Organization Name")]
    public string OrganizationName { get; init; }
    [Name("Organization Address")]
    public string OrganizationAddress { get; init; }
#nullable enable warnings
}
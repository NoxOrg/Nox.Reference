using CsvHelper.Configuration.Attributes;
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.GetData.CliCommands;

public class MacAddressInfo : IMacAddressInfo
{
#nullable disable warnings

    [Name("Assignment")]
    public string Id { get; init; }

    [Name("Assignment")]
    public string MacPrefix { get; init; }

    [Name("Registry")]
    public string IEEERegistry { get; init; }

    [Name("Organization Name")]
    public string OrganizationName { get; init; }

    [Name("Organization Address")]
    public string OrganizationAddress { get; init; }

#nullable enable warnings
}
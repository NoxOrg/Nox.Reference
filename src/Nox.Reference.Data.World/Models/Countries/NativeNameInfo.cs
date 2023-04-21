using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class NativeNameInfo : INativeNameInfo
{
    public string Language { get; set; } = string.Empty;

    public string OfficialName { get; set; } = string.Empty;

    public string CommonName { get; set; } = string.Empty;
}
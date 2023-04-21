using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class DemonymnInfo : IDemonymn
{
    public string Language { get; set; } = string.Empty;

    public string Feminine { get; set; } = string.Empty;

    public string Masculine { get; set; } = string.Empty;
}
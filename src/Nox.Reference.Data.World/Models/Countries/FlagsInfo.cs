using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class FlagsInfo : IFlags
{
    public string Svg { get; set; } = string.Empty;
    public string Png { get; set; } = string.Empty;
    public string AlternateText { get; set; } = string.Empty;
}
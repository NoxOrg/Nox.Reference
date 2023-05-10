using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CountryFlag : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Svg { get; set; } = string.Empty;
    public string Png { get; set; } = string.Empty;
    public string AlternateText { get; set; } = string.Empty;
}
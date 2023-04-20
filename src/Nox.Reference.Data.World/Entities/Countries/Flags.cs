using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class Flags : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Svg { get; set; }
    public string Png { get; set; }
    public string AlternateText { get; set; }
}
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CoatOfArms : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Svg { get; set; } = string.Empty;

    public string Png { get; set; } = string.Empty;
}
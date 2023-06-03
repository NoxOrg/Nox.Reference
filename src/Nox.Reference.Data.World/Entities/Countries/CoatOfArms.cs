using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CoatOfArms : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Svg { get; private set; } = string.Empty;

    public string Png { get; private set; } = string.Empty;
}
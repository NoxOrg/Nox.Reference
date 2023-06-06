using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class Demonymn : NoxReferenceEntityBase
{
    public virtual Language Language { get; internal set; } = new Language();
    public string Feminine { get; internal set; } = string.Empty;
    public string Masculine { get; internal set; } = string.Empty;
}
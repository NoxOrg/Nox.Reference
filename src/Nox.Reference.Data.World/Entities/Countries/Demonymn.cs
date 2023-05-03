using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class Demonymn : INoxReferenceEntity
{
    public int Id { get; private set; }
    public Language Language { get; set; } = new Language();
    public string Feminine { get; set; } = string.Empty;
    public string Masculine { get; set; } = string.Empty;
}
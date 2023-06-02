using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class LanguageTranslation : NoxReferenceEntityBase
{
    public string Translation { get; private set; } = string.Empty;
    public string Language { get; private set; } = string.Empty;
}
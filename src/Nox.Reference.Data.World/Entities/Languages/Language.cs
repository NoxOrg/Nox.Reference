using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class Language : WorldNoxReferenceEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Iso_639_1 { get; private set; }
    public string? Iso_639_2b { get; private set; }
    public string? Iso_639_2t { get; private set; }
    public string Iso_639_3 { get; private set; } = string.Empty;
    public bool Common { get; private set; }
    public LanguageType Type { get; private set; }
    public LanguageScope Scope { get; private set; }
    public virtual IReadOnlyList<LanguageTranslation> NameTranslations { get; private set; } = new List<LanguageTranslation>();
    public string? WikiUrl { get; private set; }
    public virtual IReadOnlyList<Country> Countries { get; private set; } = new List<Country>();
}
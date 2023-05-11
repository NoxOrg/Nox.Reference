using System.Collections.Generic;
using Nox.Reference.Abstractions;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class Language : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string? Iso_639_1 { get; set; }
    public string? Iso_639_2b { get; set; }
    public string? Iso_639_2t { get; set; }
    public string Iso_639_3 { get; set; } = string.Empty;
    public bool Common { get; set; }
    public LanguageType Type { get; set; }
    public LanguageScope Scope { get; set; }
    public IList<LanguageTranslation> NameTranslations { get; set; } = new List<LanguageTranslation>();
    public string? WikiUrl { get; set; }
    public IReadOnlyList<Country> Countries { get; private set; } = null!;
}
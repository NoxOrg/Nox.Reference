using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Entities.Cultures;

internal class Culture : INoxReferenceEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FormalName { get; set; } = string.Empty;
    public string NativeName { get; set; } = string.Empty;
    public string? CommonName { get; set; }
    public string Language { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string DisplayNameWithDialect { get; set; } = string.Empty;
    public string CharacterOrientation { get; set; } = string.Empty;
    public string LineOrientation { get; set; } = string.Empty;
    public string? LanguageIso_639_2t { get; set; }
    public NumberFormat NumberFormat { get; set; } = new NumberFormat();
    public DateFormat DateFormat { get; set; } = new DateFormat();
}

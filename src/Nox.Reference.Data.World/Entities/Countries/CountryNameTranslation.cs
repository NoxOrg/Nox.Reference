using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CountryNameTranslation : INoxReferenceEntity
{
    public int Id { get; private set; }
    public Country Country { get; private set; } = null!; // Only EF should resolve the relation
    public Language Language { get; set; } = new Language();
    public string OfficialName { get; set; } = string.Empty;
    public string CommonName { get; set; } = string.Empty;
}
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CountryNameTranslation : INoxReferenceEntity
{
    public int Id { get; private set; }
    public Language Language { get; set; } = new Language();
    public string OfficialName { get; set; } = string.Empty;
    public string CommonName { get; set; } = string.Empty;
}
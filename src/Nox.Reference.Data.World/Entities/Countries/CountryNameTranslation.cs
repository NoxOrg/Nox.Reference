using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CountryNameTranslation : WorldNoxReferenceEntity
{
    public virtual Country Country { get; private set; } = new Country();
    public virtual Language Language { get; internal set; } = new Language();
    public string OfficialName { get; internal set; } = string.Empty;
    public string CommonName { get; internal set; } = string.Empty;
}
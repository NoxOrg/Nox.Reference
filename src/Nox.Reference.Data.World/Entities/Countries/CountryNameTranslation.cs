namespace Nox.Reference;

public class CountryNameTranslation : NoxReferenceEntityBase,
    IDtoConvertibleEntity<CountryNameTranslationInfo>
{
    public virtual Country Country { get; private set; } = new Country();
    public virtual Language Language { get; internal set; } = new Language();
    public string OfficialName { get; internal set; } = string.Empty;
    public string CommonName { get; internal set; } = string.Empty;

    public CountryNameTranslationInfo ToDto()
    {
        return World.Mapper.Map<CountryNameTranslationInfo>(this);
    }
}
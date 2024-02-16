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
#pragma warning disable CS0618 // Type or member is obsolete
        return World.Mapper.Map<CountryNameTranslationInfo>(this);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
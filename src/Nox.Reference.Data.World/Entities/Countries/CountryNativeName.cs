namespace Nox.Reference;

public class CountryNativeName : NoxReferenceEntityBase
{
    public virtual Language Language { get; internal set; } = new Language();
    public string OfficialName { get; internal set; } = string.Empty;
    public string CommonName { get; internal set; } = string.Empty;
}
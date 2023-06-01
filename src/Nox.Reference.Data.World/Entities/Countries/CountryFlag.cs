namespace Nox.Reference.Data.World;

public class CountryFlag : WorldNoxReferenceEntity
{
    public string Svg { get; private set; } = string.Empty;
    public string Png { get; private set; } = string.Empty;
    public string AlternateText { get; private set; } = string.Empty;
}
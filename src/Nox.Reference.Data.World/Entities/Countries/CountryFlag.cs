namespace Nox.Reference;

public class CountryFlag : NoxReferenceEntityBase
{
    public string Svg { get; private set; } = string.Empty;
    public string Png { get; private set; } = string.Empty;
    public string AlternateText { get; private set; } = string.Empty;
}
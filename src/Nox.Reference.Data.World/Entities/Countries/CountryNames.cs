namespace Nox.Reference;

public class CountryNames : NoxReferenceEntityBase
{
    public string CommonName { get; private set; } = string.Empty;
    public string OfficialName { get; private set; } = string.Empty;
    public virtual IReadOnlyList<CountryNativeName> NativeNames { get; internal set; } = new List<CountryNativeName>();
}
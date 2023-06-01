using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CountryNames : WorldNoxReferenceEntity
{
    public string CommonName { get; private set; } = string.Empty;
    public string OfficialName { get; private set; } = string.Empty;
    public virtual IReadOnlyList<CountryNativeName> NativeNames { get; internal set; } = new List<CountryNativeName>();
}
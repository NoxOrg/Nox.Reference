using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CountryNames : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string CommonName { get; private set; } = string.Empty;
    public string OfficialName { get; private set; } = string.Empty;
    public IReadOnlyList<CountryNativeName> NativeNames { get; internal set; } = new List<CountryNativeName>();
}
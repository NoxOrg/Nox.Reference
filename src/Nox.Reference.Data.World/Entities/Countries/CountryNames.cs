using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CountryNames : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string CommonName { get; set; } = string.Empty;
    public string OfficialName { get; set; } = string.Empty;
    public IReadOnlyList<CountryNativeName> NativeNames { get; set; } = Array.Empty<CountryNativeName>();
}

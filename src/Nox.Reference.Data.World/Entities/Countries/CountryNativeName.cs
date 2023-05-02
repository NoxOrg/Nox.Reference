using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CountryNativeName : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Language { get; set; } = string.Empty;
    public string OfficialName { get; set; } = string.Empty;
    public string CommonName { get; set; } = string.Empty;
}
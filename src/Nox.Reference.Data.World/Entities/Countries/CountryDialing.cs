using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CountryDialing : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Prefix { get; set; } = string.Empty;

    public string Suffixes { get; set; } = string.Empty;
}
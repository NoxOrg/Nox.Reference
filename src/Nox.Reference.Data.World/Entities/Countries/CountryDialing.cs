using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CountryDialing : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Prefix { get; private set; } = string.Empty;

    public string Suffixes { get; private set; } = string.Empty;
}
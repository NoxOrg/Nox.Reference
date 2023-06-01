using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CountryDialing : WorldNoxReferenceEntity
{
    public string Prefix { get; private set; } = string.Empty;

    public string Suffixes { get; private set; } = string.Empty;
}
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class PostalCodeInfo : IPostalCodeInfo
{
    public string Format { get; set; } = string.Empty;

    public string Regex { get; set; } = string.Empty;
}
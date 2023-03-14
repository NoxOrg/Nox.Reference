namespace Nox.Reference.Countries;

public class PostalCodeInfo : IPostalCodeInfo
{
    public string Format { get; set; } = string.Empty;

    public string Regex { get; set; } = string.Empty;
}

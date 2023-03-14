
namespace Nox.Reference.Countries;

public class DialingInfo : IDialingInfo
{
    public string Prefix { get; set; } = string.Empty;

    public string[] Suffixes { get; set; } = Array.Empty<string>();
}


namespace Nox.Reference.Countries;

public class DialingInfo : IDialingInfo
{
    public string Prefix { get; set; } = string.Empty;

    public IReadOnlyList<string> Suffixes { get; set; } = new List<string>();
}

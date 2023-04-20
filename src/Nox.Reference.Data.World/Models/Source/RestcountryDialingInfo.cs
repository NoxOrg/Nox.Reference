using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Country.DataContext;

public class RestcountryDialingInfo : IDialingInfo
{
    [JsonPropertyName("root")]
    public string Prefix { get; set; } = string.Empty;

    [JsonPropertyName("suffixes")]
    public IReadOnlyList<string> Suffixes { get; set; } = new List<string>();
}
using System.Text.Json.Serialization;
using Nox.Reference.Abstractions.Countries;

namespace Nox.Reference.Country.DataContext;

public class RestcountryPostalCodeInfo : IPostalCodeInfo
{
    [JsonPropertyName("format")]
    public string Format { get; set; } = string.Empty;

    [JsonPropertyName("regex")]
    public string Regex { get; set; } = string.Empty;
}
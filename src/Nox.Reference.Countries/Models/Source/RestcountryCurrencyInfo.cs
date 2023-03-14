
using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class RestcountryCurrencyInfo : ICurrencyInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
}

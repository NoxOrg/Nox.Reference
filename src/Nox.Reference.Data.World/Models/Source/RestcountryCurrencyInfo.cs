using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Country.DataContext;

public class RestcountryCurrencyInfo : IRestcountryCurrencyInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
}
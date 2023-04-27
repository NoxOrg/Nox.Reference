using Nox.Reference.Abstractions;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

internal class LocalHolidayNameInfo : ILocalHolidayName
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;
}
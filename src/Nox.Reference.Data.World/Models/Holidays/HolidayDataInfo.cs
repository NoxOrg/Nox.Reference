using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

internal class HolidayDataInfo
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
    [JsonPropertyName("date")] public string Date { get; set; } = string.Empty;

    [JsonPropertyName("localNames")]
    public IReadOnlyList<LocalHolidayNameInfo> LocalNames { get; set; } = new List<LocalHolidayNameInfo>();
}
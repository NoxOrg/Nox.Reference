using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class RegionHolidayInfo
{
    [JsonPropertyName("region")]
    public string Region { get; set; } = "";

    [JsonPropertyName("regionName")]
    public string RegionName { get; set; } = "";

    [JsonPropertyName("holidays")]
    public IReadOnlyList<HolidayDataInfo> Holidays { get; set; } = new List<HolidayDataInfo>();
}
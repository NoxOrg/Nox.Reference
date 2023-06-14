using System.Text.Json.Serialization;

namespace Nox.Reference;

public class StateHolidayInfo
{
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("stateName")]
    public string StateName { get; set; } = string.Empty;

    [JsonPropertyName("holidays")]
    public IReadOnlyList<HolidayDataInfo> Holidays { get; set; } = new List<HolidayDataInfo>();

    [JsonPropertyName("regions")]
    public IReadOnlyList<RegionHolidayInfo> Regions { get; set; } = new List<RegionHolidayInfo>();
}
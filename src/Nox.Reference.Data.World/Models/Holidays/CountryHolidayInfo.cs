using System.Text.Json.Serialization;

namespace Nox.Reference;

public class CountryHolidayInfo
{
    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("countryName")]
    public string CountryName { get; set; } = string.Empty;

    [JsonPropertyName("dayOff")]
    public string DayOff { get; set; } = string.Empty;

    [JsonPropertyName("holidays")]
    public IReadOnlyList<HolidayDataInfo> Holidays { get; set; } = new List<HolidayDataInfo>();

    [JsonPropertyName("states")]
    public IReadOnlyList<StateHolidayInfo> States { get; set; } = new List<StateHolidayInfo>();
}
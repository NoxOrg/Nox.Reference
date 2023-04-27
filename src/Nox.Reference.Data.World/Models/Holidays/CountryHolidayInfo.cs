using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

internal class CountryHolidayInfo : ICountryHolidayInfo
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
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IReadOnlyList<IHolidayData>, HolidayDataInfo[]>))]
    public IReadOnlyList<IHolidayData> Holidays { get; set; } = new List<IHolidayData>();

    [JsonPropertyName("states")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IReadOnlyList<IStateHolidayInfo>, StateHolidayInfo[]>))]
    public IReadOnlyList<IStateHolidayInfo> States { get; set; } = new List<IStateHolidayInfo>();
}
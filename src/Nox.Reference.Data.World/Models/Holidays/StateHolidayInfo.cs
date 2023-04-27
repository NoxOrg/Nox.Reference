using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

internal class StateHolidayInfo : IStateHolidayInfo
{
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("stateName")]
    public string StateName { get; set; } = string.Empty;

    [JsonPropertyName("holidays")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IReadOnlyList<IHolidayData>, HolidayDataInfo[]>))]
    public IReadOnlyList<IHolidayData> Holidays { get; set; } = new List<IHolidayData>();

    [JsonPropertyName("regions")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IReadOnlyList<IRegionHolidayInfo>, RegionHolidayInfo[]>))]
    public IReadOnlyList<IRegionHolidayInfo> Regions { get; set; } = new List<IRegionHolidayInfo>();
}
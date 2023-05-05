using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

internal class RegionHolidayInfo : IRegionHolidayInfo
{
    [JsonPropertyName("region")]
    public string Region { get; set; } = "";

    [JsonPropertyName("regionName")]
    public string RegionName { get; set; } = "";

    [JsonPropertyName("holidays")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IReadOnlyList<IHolidayData>, HolidayDataInfo[]>))]
    public IReadOnlyList<IHolidayData> Holidays { get; set; } = new List<IHolidayData>();
}
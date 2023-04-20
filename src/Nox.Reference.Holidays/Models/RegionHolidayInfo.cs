using Nox.Reference.Abstractions;
using System.Text.Json.Serialization;

namespace Nox.Reference.Holidays.Models
{
    public class RegionHolidayInfo : IRegionHolidayInfo
    {
        [JsonPropertyName("region")] public string Region_ { get; set; } = "";
        [JsonPropertyName("regionName")] public string RegionName_ { get; set; } = "";
        [JsonPropertyName("holidays")] public List<HolidayData> Holidays_ { get; set; } = new List<HolidayData>();

        [JsonIgnore] public string Region => Region_;
        [JsonIgnore] public string RegionName => RegionName_;
        [JsonIgnore] public IReadOnlyList<IHolidayData> Holidays => Holidays_;
    }
}
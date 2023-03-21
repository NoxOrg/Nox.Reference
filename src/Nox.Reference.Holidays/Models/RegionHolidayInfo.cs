using Nox.Reference.Abstractions.Holidays;
using System.Text.Json.Serialization;

namespace Nox.Reference.Holidays.Models
{
    public class RegionHolidayInfo : IRegionHolidayInfo
    {
        [JsonPropertyName("region")] public string Region_ { get; set; } = null;
        [JsonPropertyName("regionName")] public string RegionName_ { get; set; } = null;
        [JsonPropertyName("holidays")] public List<HolidayData> Holidays_ { get; set; } = null;

        [JsonIgnore] public string Region => Region_;
        [JsonIgnore] public string RegionName => RegionName_;
        [JsonIgnore] public IReadOnlyList<IHolidayData> Holidays => Holidays_;
    }
}

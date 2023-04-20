using Nox.Reference.Abstractions;
using System.Text.Json.Serialization;

namespace Nox.Reference.Holidays.Models
{
    public class StateHolidayInfo : IStateHolidayInfo
    {
        [JsonPropertyName("state")] public string State_ { get; set; } = "";
        [JsonPropertyName("stateName")] public string StateName_ { get; set; } = "";
        [JsonPropertyName("holidays")] public List<HolidayData> Holidays_ { get; set; } = new List<HolidayData>();
        [JsonPropertyName("regions")] public List<RegionHolidayInfo> Regions_ { get; set; } = new List<RegionHolidayInfo>();

        [JsonIgnore] public string State => State_;
        [JsonIgnore] public string StateName => StateName_;
        [JsonIgnore] public IReadOnlyList<IHolidayData> Holidays => Holidays_;
        [JsonIgnore] public IReadOnlyList<IRegionHolidayInfo> Regions => Regions_;
    }
}
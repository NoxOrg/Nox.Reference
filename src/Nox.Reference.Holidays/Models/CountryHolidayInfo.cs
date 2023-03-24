using Nox.Reference.Abstractions.Holidays;
using System.Text.Json.Serialization;

namespace Nox.Reference.Holidays.Models
{
    public class CountryHolidayInfo : ICountryHolidayInfo
    {
        [JsonPropertyName("country")] public string Country_ { get; set; } = "";
        [JsonPropertyName("countryName")] public string CountryName_ { get; set; } = "";
        [JsonPropertyName("dayOff")] public string DayOff_ { get; set; } = "";
        [JsonPropertyName("holidays")] public List<HolidayData> Holidays_ { get; set; } = new List<HolidayData>();
        [JsonPropertyName("states")] public List<StateHolidayInfo> States_ { get; set; } = new List<StateHolidayInfo>();

        [JsonIgnore] public string Country => Country_;
        [JsonIgnore] public string CountryName => CountryName_;
        [JsonIgnore] public string DayOff => DayOff_;
        [JsonIgnore] public IReadOnlyList<IHolidayData> Holidays => Holidays_;
        [JsonIgnore] public IReadOnlyList<IStateHolidayInfo> States => States_;
    }
}
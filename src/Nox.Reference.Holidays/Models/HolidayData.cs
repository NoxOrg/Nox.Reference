using Nox.Reference.Abstractions;
using System.Text.Json.Serialization;

namespace Nox.Reference.Holidays.Models
{
    public class HolidayData : IHolidayData
    {
        [JsonPropertyName("name")] public string Name_ { get; set; } = "";
        [JsonPropertyName("type")] public string Type_ { get; set; } = "";
        [JsonPropertyName("date")] public string Date_ { get; set; } = "";
        [JsonPropertyName("localNames")] public List<LocalHolidayName> LocalNames_ { get; set; } = new List<LocalHolidayName>();

        [JsonIgnore] public string Name => Name_;
        [JsonIgnore] public string Type => Type_;
        [JsonIgnore] public string Date => Date_;
        [JsonIgnore] public IReadOnlyList<ILocalHolidayName> LocalNames => LocalNames_;
    }
}
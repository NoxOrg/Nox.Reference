using Nox.Reference.Abstractions.Holidays;
using System.Text.Json.Serialization;

namespace Nox.Reference.Holidays.Models
{
    public class HolidayData : IHolidayData
    {

        [JsonPropertyName("name")] public string Name_ { get; set; } = null;
        [JsonPropertyName("type")] public string Type_ { get; set; } = null;
        [JsonPropertyName("date")] public string Date_ { get; set; } = null;
        [JsonPropertyName("localNames")] public List<LocalHolidayName> LocalNames_ { get; set; } = null;

        [JsonIgnore] public string Name => Name_;
        [JsonIgnore] public string Type => Type_;
        [JsonIgnore] public string Date => Date_;
        [JsonIgnore] public IReadOnlyList<ILocalHolidayName> LocalNames => LocalNames_;
    }
}

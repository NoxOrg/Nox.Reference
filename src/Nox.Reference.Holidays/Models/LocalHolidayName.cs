using Nox.Reference.Abstractions.Holidays;
using System.Text.Json.Serialization;

namespace Nox.Reference.Holidays.Models
{
    public class LocalHolidayName : ILocalHolidayName
    {
        [JsonPropertyName("name")] public string Name_ { get; set; } = "";
        [JsonPropertyName("language")] public string Language_ { get; set; } = "";

        [JsonIgnore] public string Name => Name_;
        [JsonIgnore] public string Language => Language_;
    }
}

using Nox.Reference.Abstractions.Holidays;
using System.Text.Json.Serialization;

namespace Nox.Reference.Holidays.Models
{
    public class LocalHolidayName : ILocalHolidayName
    {
        [JsonPropertyName("name")] public string Name_ { get; set; } = null;
        [JsonPropertyName("language")] public string Language_ { get; set; } = null;

        [JsonIgnore] public string Name => Name_;
        [JsonIgnore] public string Language => Language_;
    }
}

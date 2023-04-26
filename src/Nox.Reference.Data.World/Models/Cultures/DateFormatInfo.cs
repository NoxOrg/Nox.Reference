using Nox.Reference.Abstractions.Cultures;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models.Cultures
{
    internal class DateFormatInfo : IDateFormatInfo
    {
        [JsonPropertyName("amPmStrings")] public string AmPmStrings_ { get; set; } = string.Empty;
        [JsonPropertyName("eras")] public string Eras_ { get; set; } = string.Empty;
        [JsonPropertyName("eraNames")] public string EraNames_ { get; set; } = string.Empty;
        [JsonPropertyName("months")] public string Months_ { get; set; } = string.Empty;
        [JsonPropertyName("shortMonths")] public string ShortMonths_ { get; set; } = string.Empty;
        [JsonPropertyName("shortWeekdays")] public string ShortWeekdays_ { get; set; } = string.Empty;
        [JsonPropertyName("weekdays")] public string Weekdays_ { get; set; } = string.Empty;
        [JsonPropertyName("date_3")] public string Date_3_ { get; set; } = string.Empty;
        [JsonPropertyName("date_2")] public string Date_2_ { get; set; } = string.Empty;
        [JsonPropertyName("date_1")] public string Date_1_ { get; set; } = string.Empty;
        [JsonPropertyName("date_0")] public string Date_0_ { get; set; } = string.Empty;

        [JsonIgnore] public string AmPmStrings => AmPmStrings_;
        [JsonIgnore] public string Eras => Eras_;
        [JsonIgnore] public string EraNames => EraNames_;
        [JsonIgnore] public string Months => Months_;
        [JsonIgnore] public string ShortMonths => ShortMonths_;
        [JsonIgnore] public string ShortWeekdays => ShortWeekdays_;
        [JsonIgnore] public string Weekdays => Weekdays_;
        [JsonIgnore] public string Date_3 => Date_3_;
        [JsonIgnore] public string Date_2 => Date_2_;
        [JsonIgnore] public string Date_1 => Date_1_;
        [JsonIgnore] public string Date_0 => Date_0_;
    }
}

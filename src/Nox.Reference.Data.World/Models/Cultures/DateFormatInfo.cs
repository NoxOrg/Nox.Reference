using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class DateFormatInfo
{
    [JsonPropertyName("amPmStrings")] public string AmPmStrings { get; set; } = string.Empty;
    [JsonPropertyName("eras")] public string Eras { get; set; } = string.Empty;
    [JsonPropertyName("eraNames")] public string EraNames { get; set; } = string.Empty;
    [JsonPropertyName("months")] public string Months { get; set; } = string.Empty;
    [JsonPropertyName("shortMonths")] public string ShortMonths { get; set; } = string.Empty;
    [JsonPropertyName("shortWeekdays")] public string ShortWeekdays { get; set; } = string.Empty;
    [JsonPropertyName("weekdays")] public string Weekdays { get; set; } = string.Empty;
    [JsonPropertyName("date3")] public string Date_3 { get; set; } = string.Empty;
    [JsonPropertyName("date2")] public string Date_2 { get; set; } = string.Empty;
    [JsonPropertyName("date1")] public string Date_1 { get; set; } = string.Empty;
    [JsonPropertyName("date0")] public string Date_0 { get; set; } = string.Empty;
}
using Nox.Reference.Abstractions;
using Nox.Reference.Abstractions.TimeZones;
using Nox.Reference.Common;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models.TimeZones
{
    internal class TimeZoneInfo : ITimeZoneInfo
    {
        [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
        [JsonPropertyName("embeddedComments")] public string? EmbeddedComments { get; set; }
        [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
        [JsonPropertyName("notes")] public string? Notes { get; set; }
        [JsonPropertyName("sdt_UTC_offset")] public string SDT_UTC_Offset { get; set; } = string.Empty;
        [JsonPropertyName("dst_UTC_offset")] public string DST_UTC_Offset { get; set; } = string.Empty;
        [JsonPropertyName("sdt_timeZoneAbbreviation")] public string SDT_TimeZoneAbbreviation { get; set; } = string.Empty;
        [JsonPropertyName("dst_timeZoneAbbreviation")] public string? DST_TimeZoneAbbreviation { get; set; }
        [JsonPropertyName("countriesWithTimeZone")] public List<string> CountriesWithTimeZone { get; set; } = new List<string>();

        [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IGeoCoordinates, GeoCoordinatesInfo>))]
        [JsonPropertyName("geoCoordinates")] public IGeoCoordinates? GeoCoordinates { get; set; }
    }
}
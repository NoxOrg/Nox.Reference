using Nox.Reference.Abstractions.Shared;

namespace Nox.Reference.Abstractions.TimeZones
{
    public interface ITimeZoneInfo
    {
        public string Id { get; }
        public string? EmbeddedComments { get; }
        public string Type { get; }
        public string? Notes { get; }
        public string SDT_UTC_Offset { get; }
        public string DST_UTC_Offset { get; }
        public string SDT_TimeZoneAbbreviation { get; }
        public string? DST_TimeZoneAbbreviation { get; }
        public List<string> CountriesWithTimeZone { get; }
        public IGeoCoordinates? GeoCoordinates { get; }
    }
}

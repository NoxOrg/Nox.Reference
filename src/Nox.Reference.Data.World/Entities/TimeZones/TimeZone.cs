using GeoTimeZone;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class TimeZone : NoxReferenceEntityBase,
    IDtoConvertibleEntity<Models.TimeZoneInfo>
{
    public string Code { get; set; } = string.Empty;
    public string? EmbeddedComments { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string SDT_UTC_Offset { get; set; } = string.Empty;
    public string DST_UTC_Offset { get; set; } = string.Empty;
    public string SDT_TimeZoneAbbreviation { get; set; } = string.Empty;
    public string? DST_TimeZoneAbbreviation { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public virtual IReadOnlyList<Country> Countries { get; set; } = new List<Country>();

    public static TimeZone? GetTimeZoneByCoordinates(GeoCoordinatesInfo geoCoordinates)
    {
        if (geoCoordinates?.Latitude == null ||
            geoCoordinates.Longitude == null)
        {
            throw new ArgumentException("Null coordinate value was provided.");
        }

        var result = TimeZoneLookup.GetTimeZone(Convert.ToDouble(geoCoordinates.Latitude), Convert.ToDouble(geoCoordinates.Longitude)).Result;

        return World.TimeZones.FirstOrDefault(x => x.Code == result);
    }

    public Models.TimeZoneInfo ToDto()
    {
        return World.Mapper.Map<Models.TimeZoneInfo>(this);
    }
}
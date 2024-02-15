using GeoTimeZone;

namespace Nox.Reference;

public class TimeZone : NoxReferenceEntityBase,
    IDtoConvertibleEntity<TimeZoneInfo>
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

#pragma warning disable CS0618 // Type or member is obsolete
        return World.TimeZones.FirstOrDefault(x => x.Code == result);
#pragma warning restore CS0618 // Type or member is obsolete
    }

    public TimeZoneInfo ToDto()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        return World.Mapper.Map<TimeZoneInfo>(this);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
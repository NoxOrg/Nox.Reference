namespace Nox.Reference;

internal static class TimeZoneService
{
    /// <summary>
    /// Return TimeZone object by provided geo coordinates (lat/long)
    /// </summary>
    /// <param name="geoCoordinates">Geocoordinates of a point</param>
    /// <returns>TimeZone entity framework object</returns>
    public static TimeZone? GetTimeZoneByCoordinates(GeoCoordinatesInfo geoCoordinates)
    {
        return TimeZone.GetTimeZoneByCoordinates(geoCoordinates);
    }
}
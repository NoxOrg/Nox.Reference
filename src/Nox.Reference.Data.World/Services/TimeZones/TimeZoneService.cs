namespace Nox.Reference.Data.World.Services.TimeZones
{
    internal static class TimeZoneService
    {
        public static TimeZone? GetTimeZoneByCoordinates(GeoCoordinatesInfo geoCoordinates)
        {
            return TimeZone.GetTimeZoneByCoordinates(geoCoordinates);
        }
    }
}
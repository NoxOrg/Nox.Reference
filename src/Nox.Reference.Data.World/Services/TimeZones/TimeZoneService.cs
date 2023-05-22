using TimeZoneInfo = Nox.Reference.Data.World.Models.TimeZoneInfo;

namespace Nox.Reference.Data.World.Services.TimeZones
{
    internal static class TimeZoneService
    {
        public static TimeZoneInfo? GetTimeZoneByCoordinates(GeoCoordinatesInfo geoCoordinates)
        {
            if (geoCoordinates?.Latitude == null ||
                geoCoordinates.Longitude == null)
            {
                throw new ArgumentException("Null coordinate value was provided.");
            }

            //var result = TimeZoneLookup.GetTimeZone(Convert.ToDouble(geoCoordinates.Latitude), Convert.ToDouble(geoCoordinates.Longitude)).Result;

            // TODO add when country entity is added
            return null;
        }
    }
}
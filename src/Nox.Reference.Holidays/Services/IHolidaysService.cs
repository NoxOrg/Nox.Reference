using Nox.Reference.Abstractions;

namespace Nox.Reference.Holidays
{
    public interface IHolidaysService
    {
        /// <summary>
        /// Get holidays list
        /// </summary>
        /// <returns>All supported holidays info for all countires for particular year</returns>
        IHolidayInfo GetHolidays();

        /// <summary>
        /// Get holiday info by country code
        /// </summary>
        /// <param name="isoCode">Country iso 2 code</param>
        /// <returns>All holidays for a particular country</returns>
        ICountryHolidayInfo? GetHolidaysByCountryCode(string isoCode);

        /// <summary>
        /// Get holiday info by full country name
        /// </summary>
        /// <param name="countryName">Country full name</param>
        /// <returns>All holidays for a particular country</returns>
        ICountryHolidayInfo? GetHolidaysByCountryName(string countryName);
    }
}
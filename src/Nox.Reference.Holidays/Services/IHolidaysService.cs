using Nox.Reference.Abstractions.Holidays;

namespace Nox.Reference.Holidays
{
    public interface IHolidaysService
    {
        IHolidayInfo GetHolidays();
        ICountryHolidayInfo? GetHolidaysByCountryCode(string isoCode);
        ICountryHolidayInfo? GetHolidaysByCountryName(string isoNumber);
    }
}
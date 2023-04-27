using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class HolidaysQueryExtensions
{
    public static ICountryHolidayInfo? Get(
        this IQueryable<ICountryHolidayInfo> query,
        int year,
        string countryCode)
    {
        return query.FirstOrDefault(x => x.Year == year && x.Country == countryCode);
    }
}
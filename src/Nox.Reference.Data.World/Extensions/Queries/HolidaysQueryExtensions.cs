using Nox.Reference.World;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class HolidaysQueryExtensions
{
    public static CountryHoliday? Get(
        this IQueryable<CountryHoliday> query,
        int year,
        string countryCode)
    {
        return query.FirstOrDefault(x => x.Year == year && x.Country.AlphaCode2 == countryCode);
    }

    public static CountryHoliday? Get(
        this IQueryable<CountryHoliday> query,
        int year,
        WorldCountries worldCountries)
    {
        return query.FirstOrDefault(x => x.Year == year && x.Country.EntityId == (int)worldCountries);
    }
}
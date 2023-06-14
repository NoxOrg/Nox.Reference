using Nox.Reference.Data.World.Exceptions;

namespace Nox.Reference;

public static class HolidaysQueryExtensions
{
    /// <summary>
    /// Gets holiday info
    /// <example>
    /// <code>
    /// Holidays.Get(2023, "UA")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="year">Year to get holiday info. Example: 2023.</param>
    /// <param name="countryCode">Country to get holiday info alpha code. Example: "UA".</param>
    /// <returns>Holiday info per country</returns>
    public static CountryHoliday? Get(
        this IQueryable<CountryHoliday> query,
        int year,
        string countryCode)
    {
        return query.FirstOrDefault(x => x.Year == year && x.Country.AlphaCode2 == countryCode.ToUpper());
    }

    /// <summary>
    /// Gets holiday info
    /// <example>
    /// <code>
    /// Holidays.Get(2023, WorldCountries.Austria)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="year">Year to get holiday info. Example: 2023.</param>
    /// <param name="country">Country to get holiday info enum value. Example: WorldCountries.Austria.</param>
    /// <returns>Holiday info per country</returns>
    public static CountryHoliday? Get(
        this IQueryable<CountryHoliday> query,
        int year,
        WorldCountries country)
    {
        return query.FirstOrDefault(x => x.Year == year && x.Country.Name == country.ToString());
    }

    /// <summary>
    /// Returns true if particular date is holiday date.
    /// <example>
    /// <code>
    /// var result = Holidays.IsHolidayDate(WorldCountries.Ukraine, DateTime.Parse("2023-01-04"))]
    /// var result = Holidays.IsHolidayDate(WorldCountries.Ukraine, DateTime.Parse("2023-01-02"))]
    /// var result = Holidays.IsHolidayDate(WorldCountries.UnitedStates, DateTime.Parse("2023-02-20"), "AL")]
    /// var result = Holidays.IsHolidayDate(WorldCountries.UnitedStates, DateTime.Parse("2023-02-20"), "Alabama")]
    /// var result = Holidays.IsHolidayDate(WorldCountries.Venezuela, DateTime.Parse("2023-01-06"), "K", "B")]
    /// var result = Holidays.IsHolidayDate(WorldCountries.Venezuela, DateTime.Parse("2023-01-06"), "Lara", "Anzoátegui")]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Holidays list</param>
    /// <param name="date">Date to check for holiday</param>
    /// <param name="country">country enum value</param>
    /// <param name="state">State code. Can be a number string or text depending on a country.</param>
    /// <param name="region">State code. Can be a number string or text depending on a country.</param>
    /// <returns>True if date has a holiday, false if not</returns>
    /// <exception cref="HolidayCountryNotFoundException">This exception is thrown if no country holiday data was found for given country/year combination.</exception>
    /// <exception cref="HolidayRegionNotFoundException">This exception is thrown if no special holiday conditions are supported for a given state/region.</exception>
    public static bool IsHolidayDate(
        this IQueryable<CountryHoliday> query,
        WorldCountries country,
        DateTime date,
        string? state = null,
        string? region = null)
    {
        return query.GetHoliday(country, date, state, region) != null;
    }

    /// <summary>
    /// Returns true if particular date is holiday date.
    /// <example>
    /// <code>
    /// var result = Holidays.IsHolidayDate("UA", DateTime.Parse("2023-01-04"))]
    /// var result = Holidays.IsHolidayDate("UA", DateTime.Parse("2023-01-02"))]
    /// var result = Holidays.IsHolidayDate("US", DateTime.Parse("2023-02-20"), "AL")]
    /// var result = Holidays.IsHolidayDate("US", DateTime.Parse("2023-02-20"), "Alabama")]
    /// var result = Holidays.IsHolidayDate("VE", DateTime.Parse("2023-01-06"), "K", "B")]
    /// var result = Holidays.IsHolidayDate("VE", DateTime.Parse("2023-01-06"), "Lara", "Anzoátegui")]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Holidays list</param>
    /// <param name="date">Date to check for holiday</param>
    /// <param name="country">Country alpha 2 code</param>
    /// <param name="state">State code. Can be a number string or text depending on a country.</param>
    /// <param name="region">State code. Can be a number string or text depending on a country.</param>
    /// <returns>True if date has a holiday, false if not</returns>
    /// <exception cref="HolidayCountryNotFoundException">This exception is thrown if no country holiday data was found for given country/year combination.</exception>
    /// <exception cref="HolidayRegionNotFoundException">This exception is thrown if no special holiday conditions are supported for a given state/region.</exception>
    public static bool IsHolidayDate(
        this IQueryable<CountryHoliday> query,
        string country,
        DateTime date,
        string? state = null,
        string? region = null)
    {
        return query.GetHoliday(country.ToString(), date, state, region) != null;
    }

    /// <summary>
    /// Returns holiday for a particular date if present. If no holiday found returns null, if no region/state found in special holidays list, throws an exception.
    /// <example>
    /// <code>
    /// Holidays.GetHoliday(WorldCountries.Ukraine, DateTime.Parse("2023-01-04"))]
    /// Holidays.GetHoliday(WorldCountries.Ukraine, DateTime.Parse("2023-01-02"))]
    /// Holidays.GetHoliday(WorldCountries.UnitedStates, DateTime.Parse("2023-02-20"), "AL")]
    /// Holidays.GetHoliday(WorldCountries.UnitedStates, DateTime.Parse("2023-02-20"), "Alabama")]
    /// Holidays.GetHoliday(WorldCountries.Venezuela, DateTime.Parse("2023-01-06"), "K", "B")]
    /// Holidays.GetHoliday(WorldCountries.Venezuela, DateTime.Parse("2023-01-06"), "Lara", "Anzoátegui")]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Holidays list</param>
    /// <param name="date">Date to check for holiday</param>
    /// <param name="country">Country enum value</param>
    /// <param name="state">State code. Can be a number string or text depending on a country.</param>
    /// <param name="region">State code. Can be a number string or text depending on a country.</param>
    /// <returns>Found holiday or null</returns>
    /// <exception cref="HolidayCountryNotFoundException">This exception is thrown if no country holiday data was found for given country/year combination.</exception>
    /// <exception cref="HolidayRegionNotFoundException">This exception is thrown if no special holiday conditions are supported for a given state/region.</exception>
    public static HolidayData? GetHoliday(
        this IQueryable<CountryHoliday> query,
        WorldCountries country,
        DateTime date,
        string? state = null,
        string? region = null)
    {
        var countryCode = World.Countries.Get(country)!;
        return query.GetHoliday(countryCode.Code, date, state, region);
    }

    /// <summary>
    /// Returns holiday for a particular date if present. If no holiday found returns null, if no region/state found in special holidays list, throws an exception.
    /// <example>
    /// <code>
    /// Holidays.GetHoliday("UA", DateTime.Parse("2023-01-04"))]
    /// Holidays.GetHoliday("UA", DateTime.Parse("2023-01-02"))]
    /// Holidays.GetHoliday("US", DateTime.Parse("2023-02-20"), "AL")]
    /// Holidays.GetHoliday("US", DateTime.Parse("2023-02-20"), "Alabama")]
    /// Holidays.GetHoliday("VE", DateTime.Parse("2023-01-06"), "K", "B")]
    /// Holidays.GetHoliday("VE", DateTime.Parse("2023-01-06"), "Lara", "Anzoátegui")]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Holidays list</param>
    /// <param name="date">Date to check for holiday</param>
    /// <param name="country">Country alpha 2 code</param>
    /// <param name="state">State code. Can be a number string or text depending on a country.</param>
    /// <param name="region">State code. Can be a number string or text depending on a country.</param>
    /// <returns>Found holiday or null</returns>
    /// <exception cref="HolidayCountryNotFoundException">This exception is thrown if no country holiday data was found for given country/year combination.</exception>
    /// <exception cref="HolidayRegionNotFoundException">This exception is thrown if no special holiday conditions are supported for a given state/region.</exception>
    public static HolidayData? GetHoliday(
        this IQueryable<CountryHoliday> query,
        string country,
        DateTime date,
        string? state = null,
        string? region = null)
    {
        var all = query.ToList();
        var countryHoliday = query.FirstOrDefault(x => x.Year == date.Year && x.Country.Code == country);
        if (countryHoliday == null)
        {
            throw new HolidayCountryNotFoundException($"Holidays for country '{country}' not found for given year. Currently, only 2023, 2024, 2025 years are supported");
        }

        HolidayData? holiday = null;
        var formattedDate = date.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

        if (!string.IsNullOrWhiteSpace(state))
        {
            var stateHolidays = countryHoliday.States.FirstOrDefault(x => x.State.ToLowerInvariant() == state.ToLowerInvariant() || x.StateName.ToLowerInvariant() == state.ToLowerInvariant());
            if (stateHolidays == null)
            {
                throw new HolidayRegionNotFoundException($"State '{state}' for country '{country}' has no special holidays.");
            }

            if (!string.IsNullOrWhiteSpace(region))
            {
                var regionHolidays = stateHolidays.Regions.FirstOrDefault(x => x.Region.ToLowerInvariant() == region.ToLowerInvariant() || x.RegionName.ToLowerInvariant() == region.ToLowerInvariant());
                if (regionHolidays == null)
                {
                    throw new HolidayRegionNotFoundException($"Region '{region}' for state '{state}' has no special holidays.");
                }
                else
                {
                    holiday = regionHolidays.Holidays.FirstOrDefault(x => x.Date == formattedDate);

                    return holiday;
                }
            }
            else
            {
                holiday = stateHolidays.Holidays.FirstOrDefault(x => x.Date == formattedDate);

                return holiday;
            }
        }

        holiday = countryHoliday.Holidays.FirstOrDefault(x => x.Date == formattedDate);

        return holiday;
    }
}
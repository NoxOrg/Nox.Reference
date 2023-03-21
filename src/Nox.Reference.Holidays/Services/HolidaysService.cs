using Nox.Reference.Abstractions.Holidays;
using Nox.Reference.Holidays.Models;
using System.Reflection;
using System.Text.Json;

namespace Nox.Reference.Holidays;

public class HolidaysService : IHolidaysService
{
    private readonly IHolidayInfo _holidays = null;
    private readonly Dictionary<string, ICountryHolidayInfo> _holidayInfoByCountryIsoCode = new Dictionary<string, ICountryHolidayInfo>();
    private readonly Dictionary<string, ICountryHolidayInfo> _holidayInfoCountryName = new Dictionary<string, ICountryHolidayInfo>();
    private readonly IReadOnlySet<int> _availableYears = new HashSet<int> { 2023, 2024 };

    public HolidaysService(int year)
    {
        if (!_availableYears.Contains(year))
        {
            throw new ArgumentException($"Provided year {year} was not found in available years list. Please, use one of the following: {string.Join(',', _availableYears.ToArray())}");
        }

        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"Nox.Reference.Holidays-{year}.json";
        if (assembly == null)
        {
            return;
        }

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return;
        }

        using var reader = new StreamReader(stream);

        _holidays = JsonSerializer.Deserialize<HolidayInfo>(
            reader.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new HolidayInfo();

        foreach (var holiday in _holidays.HolidaysByCountries)
        {
            _holidayInfoByCountryIsoCode[holiday.Country] = holiday;
            _holidayInfoCountryName[holiday.CountryName] = holiday;
        }
    }

    public IHolidayInfo GetHolidays() => _holidays;

    public ICountryHolidayInfo? GetHolidaysByCountryCode(string countryIsoCode)
    {
        _holidayInfoByCountryIsoCode.TryGetValue(countryIsoCode, out ICountryHolidayInfo? holidays);
        return holidays;
    }

    public ICountryHolidayInfo? GetHolidaysByCountryName(string countryName)
    {
        _holidayInfoCountryName.TryGetValue(countryName, out ICountryHolidayInfo? holidays);
        return holidays;
    }
}

using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions
{
    public interface IHolidayInfo
    {
        public int Year { get; }
        public IReadOnlyList<ICountryHolidayInfo> HolidaysByCountries { get; }
    }
}

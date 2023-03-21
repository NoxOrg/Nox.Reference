﻿using Nox.Reference.Abstractions.Holidays;
using System.Text.Json.Serialization;

namespace Nox.Reference.Holidays.Models
{
    public class HolidayInfo : IHolidayInfo
    {
        [JsonPropertyName("year")] public int Year_ { get; set; } = -1;
        [JsonPropertyName("holidaysByCountries")] public List<CountryHolidayInfo> HolidaysByCountries_ { get; set; } = null;

        [JsonIgnore] public int Year => Year_;
        [JsonIgnore] public IReadOnlyList<ICountryHolidayInfo> HolidaysByCountries => HolidaysByCountries_;
    }
}
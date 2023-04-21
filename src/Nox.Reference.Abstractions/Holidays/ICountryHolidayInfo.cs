﻿namespace Nox.Reference.Abstractions
{
    public interface ICountryHolidayInfo
    {
        public string Country { get; }
        public string CountryName { get; }
        public string DayOff { get; }
        public IReadOnlyList<IHolidayData> Holidays { get; }
        public IReadOnlyList<IStateHolidayInfo> States { get; }
    }
}
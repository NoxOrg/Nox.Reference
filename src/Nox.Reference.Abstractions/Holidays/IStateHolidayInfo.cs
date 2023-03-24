namespace Nox.Reference.Abstractions.Holidays
{
    public interface IStateHolidayInfo
    {
        public string State { get; }
        public string StateName { get; }
        public IReadOnlyList<IHolidayData> Holidays { get; }
        public IReadOnlyList<IRegionHolidayInfo> Regions { get; }
    }
}

namespace Nox.Reference.Abstractions.Holidays
{
    public interface IRegionHolidayInfo
    {
        public string Region { get; }
        public string RegionName { get; }
        public IReadOnlyList<IHolidayData> Holidays { get; }
    }
}

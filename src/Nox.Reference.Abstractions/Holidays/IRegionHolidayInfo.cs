namespace Nox.Reference.Abstractions
{
    public interface IRegionHolidayInfo
    {
        public string Region { get; }
        public string RegionName { get; }
        public IReadOnlyList<IHolidayData> Holidays { get; }
    }
}

namespace Nox.Reference.Abstractions.Holidays
{
    public interface IHolidayData
    {
        public string Name { get; }
        public string Type { get; }
        public string Date { get; }
        public IReadOnlyList<ILocalHolidayName> LocalNames { get; }
    }
}

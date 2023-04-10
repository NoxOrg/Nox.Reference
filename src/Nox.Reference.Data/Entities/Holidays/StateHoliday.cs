namespace Nox.Reference.Data
{
    internal class StateHoliday : INoxReferenceEntity
    {
        public int Id { get; private set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public IReadOnlyList<HolidayData> Holidays { get; set; }
        public IReadOnlyList<RegionHoliday> Regions { get; set; }
    }
}
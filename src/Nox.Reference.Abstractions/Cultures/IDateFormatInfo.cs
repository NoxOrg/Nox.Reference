namespace Nox.Reference.Abstractions.Cultures
{
    public interface IDateFormatInfo
    {
        public string AmPmStrings { get; }
        public string Eras { get; }
        public string EraNames { get; }
        public string Months { get; }
        public string ShortMonths { get; }
        public string ShortWeekdays { get; }
        public string Weekdays { get; }
        public string Date_3 { get; }
        public string Date_2 { get; }
        public string Date_1 { get; }
        public string Date_0 { get; }
    }
}

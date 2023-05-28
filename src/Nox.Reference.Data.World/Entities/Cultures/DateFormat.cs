using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class DateFormat : INoxReferenceEntity
{
    public int Id { get; set; }
    public string AmPmStrings { get; private set; } = string.Empty;
    public string Eras { get; private set; } = string.Empty;
    public string EraNames { get; private set; } = string.Empty;
    public string Months { get; private set; } = string.Empty;
    public string ShortMonths { get; private set; } = string.Empty;
    public string ShortWeekdays { get; private set; } = string.Empty;
    public string Weekdays { get; private set; } = string.Empty;
    public string Date_3 { get; private set; } = string.Empty;
    public string Date_2 { get; private set; } = string.Empty;
    public string Date_1 { get; private set; } = string.Empty;
    public string Date_0 { get; private set; } = string.Empty;
    public virtual Culture Culture { get; private set; } = new Culture();
}
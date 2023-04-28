using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Entities.Cultures;

internal class DateFormat : INoxReferenceEntity
{
    public int Id { get; set; }
    public string AmPmStrings { get; set; } = string.Empty;
    public string Eras { get; set; } = string.Empty;
    public string EraNames { get; set; } = string.Empty;
    public string Months { get; set; } = string.Empty;
    public string ShortMonths { get; set; } = string.Empty;
    public string ShortWeekdays { get; set; } = string.Empty;
    public string Weekdays { get; set; } = string.Empty;
    public string Date_3 { get; set; } = string.Empty;
    public string Date_2 { get; set; } = string.Empty;
    public string Date_1 { get; set; } = string.Empty;
    public string Date_0 { get; set; } = string.Empty;
    public Culture? Culture { get; set; }
}

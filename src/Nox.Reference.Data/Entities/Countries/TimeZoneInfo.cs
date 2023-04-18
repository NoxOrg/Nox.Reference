using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext;

internal class TimeZoneInfo : INoxReferenceEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}
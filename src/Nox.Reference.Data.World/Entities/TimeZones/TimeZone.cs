using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class TimeZone : INoxReferenceEntity
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? EmbeddedComments { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string SDT_UTC_Offset { get; set; } = string.Empty;
    public string DST_UTC_Offset { get; set; } = string.Empty;
    public string SDT_TimeZoneAbbreviation { get; set; } = string.Empty;
    public string? DST_TimeZoneAbbreviation { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    // TODO: add relation to country entity when it's created
}
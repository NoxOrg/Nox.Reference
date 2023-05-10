using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

internal class HolidayDataInfo : IHolidayData
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
    [JsonPropertyName("date")] public string Date { get; set; } = string.Empty;

    [JsonPropertyName("localNames")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IReadOnlyList<ILocalHolidayName>, LocalHolidayNameInfo[]>))]
    public IReadOnlyList<ILocalHolidayName> LocalNames { get; set; } = new List<ILocalHolidayName>();
}
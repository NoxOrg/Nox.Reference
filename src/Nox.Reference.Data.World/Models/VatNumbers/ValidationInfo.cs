using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

internal class ValidationInfo
{
    [JsonPropertyName("translationId")]
    public string TranslationId { get; set; } = string.Empty;

    [JsonPropertyName("regex")]
    public string Regex { get; set; } = string.Empty;

    [JsonPropertyName("validationFormatDescription")]
    public string ValidationFormatDescription { get; set; } = string.Empty;

    [JsonPropertyName("inputMask")]
    public string InputMask { get; set; } = string.Empty;

    [JsonPropertyName("minimumLength")]
    public int MinimumLength { get; set; }

    [JsonPropertyName("maximumLength")]
    public int MaximumLength { get; set; }

    [JsonPropertyName("checksum")]
    public ChecksumInfo? Checksum { get; set; }
}
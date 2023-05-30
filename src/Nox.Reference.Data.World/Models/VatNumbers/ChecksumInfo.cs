using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class ChecksumInfo
{
    [JsonPropertyName("algorithm")] public ChecksumAlgorithm? Algorithm { get; set; }
    [JsonPropertyName("checksumDigit")] public string? ChecksumDigit { get; set; }
    [JsonPropertyName("modulus")] public int? Modulus { get; set; }
    [JsonPropertyName("weights")] public List<int>? Weights { get; set; }
}
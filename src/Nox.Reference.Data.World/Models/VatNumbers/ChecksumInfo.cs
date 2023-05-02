using Nox.Reference.Abstractions;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

public class ChecksumInfo : IChecksumInfo
{
    [JsonPropertyName("algorithm")] public ChecksumAlgorithm? Algorithm { get; set; }
    [JsonPropertyName("checksumDigit")] public string? ChecksumDigit { get; set; }
    [JsonPropertyName("modulus")] public int? Modulus { get; set; }
    [JsonPropertyName("weights")] public List<int>? Weights { get; set; }
}
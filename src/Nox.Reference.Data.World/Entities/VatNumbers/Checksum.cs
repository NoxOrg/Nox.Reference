namespace Nox.Reference;

public class Checksum
{
    public ChecksumAlgorithm? Algorithm { get; private set; }
    public string? ChecksumDigit { get; private set; }
    public int? Modulus { get; private set; }
    public string Weights { get; private set; } = string.Empty;

    public int[] GetWeights()
    {
        if (string.IsNullOrEmpty(Weights))
        {
            return Array.Empty<int>();
        }

        return Weights.Split(",")
            .Select(x => int.Parse(x))
            .ToArray();
    }
}
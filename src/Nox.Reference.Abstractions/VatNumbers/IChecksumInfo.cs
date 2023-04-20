namespace Nox.Reference.Abstractions
{
    public interface IChecksumInfo
    {
        public ChecksumAlgorithm? Algorithm { get; set; }
        public string? ChecksumDigit { get; set; }
        public int? Modulus { get; set; }
        public List<int>? Weights { get; set; }
    }
}

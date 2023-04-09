namespace Nox.Reference.Abstractions.VatNumbers
{
    public interface IChecksumInfo
    {
        public ChecksumAlgorithm? Algorithm { get; set; }
        public string? ChecksumDigit { get; set; }
        public int? Modulus { get; set; }
        public List<int>? Weights { get; set; }
    }
}

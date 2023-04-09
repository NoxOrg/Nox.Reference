namespace Nox.Reference.Abstractions.VatNumbers
{
    public interface IValidationInfo
    {
        public string Regex { get; }
        public string ValidationFormatDescription { get; set; }
        public string InputMask { get; }
        public int MinimumLength { get; }
        public int MaximumLength { get; }
        public IChecksumInfo? Checksum { get; }
    }
}

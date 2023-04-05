namespace Nox.Reference.Abstractions.VatNumbers
{
    public interface IValidationInfo
    {
        public bool IsUsingManuallyImplementedValidation { get; set; }
        public bool IsUsingNullCheck { get; set; }
        public bool IsUsingRegexValidation { get; set; }
        public bool IsUsingChecksumValidation { get; set; }
        public string ChecksumAlgorithm { get; set; }
        public int MinimumLengthCheck { get; set; }
        public List<int> Weights { get; set; }
        public int NumberToDivideBy { get; set; }
        public string СheckDigitPosition { get; set; }
    }
}

namespace Nox.Reference.VatNumbers.Constants
{
    public static class ValidationErrors
    {
        public const string NullValueError = "Null value was provided.";
        public const string ValidatorNotFoundError = "Cannot find validator for a particular error.";
        public const string WrongFormatErrorTemplate = "Validation of given value '{0}' has failed. Please, use the following format: '{1}'.";
        public const string ChecksumError = "Checksum validation has failed.";
        public const string NumberShouldConsistOfDigits = "VAT number should consist of digits.";
        public const string MinimumNumbericLengthError = "VAT number should have at least '{0}' numeric symbols";
        public const string LengthShouldEqualError = "VAT number should have exactly '{0}' numeric symbols";
        public const string UnknownFormat = "Provided VAT number format is unknown.";

        // GB
        public const string GB_InvalidGDVat = "Invalid GD value was provided. First 3 digits should be less than 500.";
        public const string GB_InvalidHAVat = "Invalid GD value was provided. First 3 digits should be more or equal 500.";
    }
}

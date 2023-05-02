namespace Nox.Reference.Data.World;

public static class ValidationErrors
{
    public const string NullValueError = "Null value was provided.";
    public const string EmptyCountryError = "Empty country value was provided.";
    public const string UnknownChecksumAlgorithm = "Unknown checksum algorithm.";
    public const string EmptyVatNumberError = "Empty vat number was provided.";
    public const string ValidatorNotFoundError = "Cannot find validator for a provided Vat number.";
    public const string WrongFormatErrorTemplate = "Validation of given value '{0}' has failed. Please, use the following format: '{1}'.";
    public const string ChecksumError = "Checksum validation has failed.";
    public const string NumberShouldConsistOfDigits = "VAT number should consist of digits.";
    public const string MinimumNumbericLengthError = "VAT number should have at least '{0}' numeric symbols";
    public const string MaximumNumbericLengthError = "VAT number should be no longer than '{0}' numeric symbols";
    public const string LengthShouldEqualError = "VAT number should have exactly '{0}' numeric symbols";
    public const string UnknownFormat = "Provided VAT number format is unknown.";
    public const string LuhnDigitChecksumValidationError = "Luhn digit checksum validation failed.";
    public const string SymbolShouldEqualError = "Symbol on index '{0}' has wrong value. Expected: '{1}'. Actual: '{2}'.";
    public const string CantMatchValidationPatternError = "Can't match any validation pattern for the provided value.";
    public const string NotEnoughParametersProvidedToChecksum = "Not enough parameters were provided to calculate checksum. Please, check that these mandatory parameters are provided: {0}.";
    public const string WeidghtsTooShortError = "Weigths array is longer than provided string.";
    public const string ChecksumShoulBeBiggerThanZero = "Checksum number should be bigger than zero.";
    public const string ChecksumDigitPositionNotNumeric = "Checksum digit position should be a number.";
    public const string ApiValidationError = "API validation has failed.";

    // GB
    public const string GB_InvalidGDVat = "Invalid government department value was provided. First 3 digits should be less than 500.";

    public const string GB_InvalidHAVat = "Invalid health authority value was provided. First 3 digits should be more or equal 500.";

    // MX
    public const string MX_InvalidDate = "Vat has invalid date.";
}
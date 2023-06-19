using System.Text;

namespace Nox.Reference;

public class TaxNumberValidationResult
{
    private readonly List<string?> _validationErrors = new();

    private TaxNumberValidationResult()
    {
    }

    public ValidationStatus Status { get; private set; } = ValidationStatus.Unverified;
    public object? ApiVerificationData { get; set; }
    public IReadOnlyList<string?> ValidationErrors => _validationErrors;
    public string FormattedTaxNumber { get; init; } = string.Empty;
    public string OriginalTaxNumber { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;

    public static TaxNumberValidationResult CreateWithoutValidation(string validationError)
    {
        var result = new TaxNumberValidationResult();
        if (!string.IsNullOrWhiteSpace(validationError))
        {
            result.AddErrorIfNotEmpty(validationError);
        }

        result.Status = ValidationStatus.Unverified;
        return result;
    }

    public static TaxNumberValidationResult CreateWithValidation(string taxNumber, string country)
    {
        var result = new TaxNumberValidationResult
        {
            OriginalTaxNumber = taxNumber,
            FormattedTaxNumber = NormalizeTaxNumber(taxNumber, country),
            Country = country,
            Status = ValidationStatus.Valid
        };
        return result;
    }

    public void AddErrorIfNotEmpty(string error)
    {
        if (error != null)
        {
            Status = ValidationStatus.Invalid;
        }
        _validationErrors.Add(error);
    }

    public void AddErrorsIfNotEmpty(IEnumerable<string> errors)
    {
        if (errors == null || !errors.Any())
        {
            return;
        }

        Status = ValidationStatus.Invalid;
        _validationErrors.AddRange(errors);
    }

    private static string NormalizeTaxNumber(string taxNumber, string country)
    {
        var number = RemoveSpecialCharacters(taxNumber);

        var numberWithoutCountryCode = number.ToUpper().TrimStart(country.ToCharArray());
        return $"{country.ToUpper()}{numberWithoutCountryCode}";
    }

    private static string RemoveSpecialCharacters(string taxNumber)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < taxNumber?.Length; i++)
        {
            if (char.IsLetterOrDigit(taxNumber[i]))
            {
                sb.Append(taxNumber[i]);
            }
        }

        return sb.ToString();
    }
}
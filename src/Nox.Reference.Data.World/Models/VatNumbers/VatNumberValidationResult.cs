using System.Text;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

internal class VatNumberValidationResult : IVatNumberValidationResult
{
    private readonly List<string?> _validationErrors = new();

    private VatNumberValidationResult()
    {
    }

    public static VatNumberValidationResult CreateWithoutValidation(string validationError)
    {
        var result = new VatNumberValidationResult();
        if (!string.IsNullOrWhiteSpace(validationError))
        {
            result.AddError(validationError);
        }
        return result;
    }

    public static VatNumberValidationResult CreateWithValidaton(string vatNumber, string country)
    {
        var result = new VatNumberValidationResult
        {
            OriginalVatNumber = vatNumber,
            FormattedVatNumber = NormalizeVatNumber(vatNumber, country),
            IsVerified = true,
            Country = country
        };
        return result;
    }

    public bool IsValid => _validationErrors.Count == 0;
    public bool IsVerified { get; init; }
    public object? ApiVerificationData { get; set; }
    public IReadOnlyList<string?> ValidationErrors => _validationErrors;
    public string FormattedVatNumber { get; init; } = string.Empty;
    public string OriginalVatNumber { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;

    public void AddError(string error)
    {
        _validationErrors.Add(error);
    }

    public void AddErrors(IEnumerable<string> errors)
    {
        _validationErrors.AddRange(errors);
    }

    private static string NormalizeVatNumber(string vatNumber, string country)
    {
        var number = RemoveSpecialCharacthers(vatNumber);

        var numberWithoutCountryCode = number.ToUpper().TrimStart(country.ToCharArray());
        return $"{country.ToUpper()}{numberWithoutCountryCode}";
    }

    private static string RemoveSpecialCharacthers(string vatNumber)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < vatNumber?.Length; i++)
        {
            if (char.IsLetterOrDigit(vatNumber[i]))
            {
                sb.Append(vatNumber[i]);
            }
        }

        return sb.ToString();
    }
}
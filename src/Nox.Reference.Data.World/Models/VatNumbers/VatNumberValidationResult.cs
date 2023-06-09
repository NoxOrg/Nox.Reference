﻿using System.Text;

namespace Nox.Reference;

public class VatNumberValidationResult
{
    private readonly List<string?> _validationErrors = new();

    private VatNumberValidationResult()
    {
    }

    public ValidationStatus Status { get; private set; } = ValidationStatus.Unverified;
    public object? ApiVerificationData { get; set; }
    public IReadOnlyList<string?> ValidationErrors => _validationErrors;
    public string FormattedVatNumber { get; init; } = string.Empty;
    public string OriginalVatNumber { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;

    public static VatNumberValidationResult CreateWithoutValidation(string validationError)
    {
        var result = new VatNumberValidationResult();
        if (!string.IsNullOrWhiteSpace(validationError))
        {
            result.AddError(validationError);
        }

        result.Status = ValidationStatus.Unverified;
        return result;
    }

    public static VatNumberValidationResult CreateWithValidaton(string vatNumber, string country)
    {
        var result = new VatNumberValidationResult
        {
            OriginalVatNumber = vatNumber,
            FormattedVatNumber = NormalizeVatNumber(vatNumber, country),
            Country = country,
            Status = ValidationStatus.Valid
        };
        return result;
    }

    public void AddError(string error)
    {
        if (error != null)
        {
            Status = ValidationStatus.Invalid;
        }
        _validationErrors.Add(error);
    }

    public void AddErrors(IEnumerable<string> errors)
    {
        if (errors == null || !errors.Any())
        {
            return;
        }

        Status = ValidationStatus.Invalid;
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
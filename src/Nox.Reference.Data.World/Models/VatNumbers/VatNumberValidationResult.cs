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

    public static VatNumberValidationResult CreateWithValidaton(string formattedVatNumber)
    {
        var result = new VatNumberValidationResult
        {
            FormattedVatNumber = formattedVatNumber,
            IsVerified = true
        };
        return result;
    }

    public bool IsValid => _validationErrors.Count == 0;
    public bool IsVerified { get; init; }
    public object? ApiVerificationData { get; set; }
    public IReadOnlyList<string?> ValidationErrors => _validationErrors;
    public string FormattedVatNumber { get; init; } = string.Empty;

    public void AddError(string error)
    {
        _validationErrors.Add(error);
    }

    public void AddErrors(IEnumerable<string> errors)
    {
        _validationErrors.AddRange(errors);
    }
}
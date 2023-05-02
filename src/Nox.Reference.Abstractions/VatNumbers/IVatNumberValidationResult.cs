namespace Nox.Reference.Abstractions;

public interface IVatNumberValidationResult
{
    string FormattedVatNumber { get; }
    string OriginalVatNumber { get; }
    string Country { get; }
    VatValidationStatus Status { get; }
    IReadOnlyList<string?> ValidationErrors { get; }
    object? ApiVerificationData { get; }
}
namespace Nox.Reference.Abstractions;

public interface IVatNumberValidationResult
{
    bool IsValid { get; }
    string FormattedVatNumber { get; }
    string OriginalVatNumber { get; }
    string Country { get; }
    bool IsVerified { get; }
    IReadOnlyList<string?> ValidationErrors { get; }
    object? ApiVerificationData { get; }
}
namespace Nox.Reference.Abstractions;

public interface IVatNumberValidationResult
{
    bool IsValid { get; }
    string FormattedVatNumber { get; }
    bool IsVerified { get; }
    IReadOnlyList<string?> ValidationErrors { get; }
    object? ApiVerificationData { get; }
}
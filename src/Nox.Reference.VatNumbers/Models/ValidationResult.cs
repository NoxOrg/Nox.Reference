namespace Nox.Reference.VatNumbers.Models
{
    // TODO maybe make record?
    public class ValidationResult
    {
        public ValidationStatus ValidationStatus => ValidationErrors?.Count == 0 ? ValidationStatus.Valid : ValidationStatus.Invalid;
        public List<string?> ValidationErrors { get; } = new List<string?>();

        public ValidationResult(string? validationError = null)
        {
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                ValidationErrors.Add(validationError);
            }
        }
    }
}

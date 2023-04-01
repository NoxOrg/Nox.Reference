namespace Nox.Reference.VatNumbers.Models
{
    // TODO maybe make record?
    public class ValidationResult
    {
        public bool IsValid => ValidationErrors?.Count == 0;
        public List<string?> ValidationErrors { get; } = new List<string?>();

        public ValidationResult() { }

        public ValidationResult(string? validationError)
        {
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                ValidationErrors.Add(validationError);
            }
        }
    }
}

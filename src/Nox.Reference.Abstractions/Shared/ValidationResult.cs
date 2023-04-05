namespace Nox.Reference.Shared
{
    // TODO maybe make record?
    // TODO: think about visibility of pre-implemented classes that we want to provide
    // Should they be public or internal?
    public class ValidationResult : IValidationResult
    {
        public bool IsValid => ValidationErrors?.Count == 0;
        public List<string?> ValidationErrors { get; } = new List<string?>();
        IList<string?> IValidationResult.ValidationErrors => ValidationErrors;

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

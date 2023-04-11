namespace Nox.Reference.Shared
{
    public interface IValidationResult
    {
        public bool IsValid { get; }
        public IList<string?> ValidationErrors { get; }
    }
}

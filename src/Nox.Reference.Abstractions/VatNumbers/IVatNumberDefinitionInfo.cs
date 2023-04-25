namespace Nox.Reference.Abstractions
{
    public interface IVatNumberDefinitionInfo
    {
        public string Country { get; set; }
        public string LocalName { get; set; }
        public VerificationApi VerificationApi { get; set; }
        public IValidationInfo[]? Validations { get; set; }
    }
}
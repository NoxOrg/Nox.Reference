using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class VatNumberDefinition : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Country { get; private set; } = string.Empty;
    public string LocalName { get; private set; } = string.Empty;
    public VerificationApi VerificationApi { get; private set; }
    public IReadOnlyList<VatNumberValidationRule> ValidationRules { get; private set; } = new List<VatNumberValidationRule>();
}
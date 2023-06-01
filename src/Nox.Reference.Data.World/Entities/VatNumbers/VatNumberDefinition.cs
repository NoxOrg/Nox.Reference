using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class VatNumberDefinition : WorldNoxReferenceEntity
{
    public string Country { get; private set; } = string.Empty;
    public string LocalName { get; private set; } = string.Empty;
    public VerificationApi VerificationApi { get; private set; }
    public virtual IReadOnlyList<VatNumberValidationRule> ValidationRules { get; private set; } = new List<VatNumberValidationRule>();
}
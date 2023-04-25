using Nox.Reference.Abstractions;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class VatNumberDefinition : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Country { get; set; } = string.Empty;
    public string LocalName { get; set; } = string.Empty;
    public VerificationApi VerificationApi { get; set; }
    public IReadOnlyList<VatNumberValidationRule> ValidationRules { get; set; } = new List<VatNumberValidationRule>();
}
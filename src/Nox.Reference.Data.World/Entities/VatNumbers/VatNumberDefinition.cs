namespace Nox.Reference;

public class VatNumberDefinition : NoxReferenceEntityBase,
    IDtoConvertibleEntity<VatNumberDefinitionInfo>
{
    public string LocalName { get; private set; } = string.Empty;
    public VerificationApi VerificationApi { get; private set; }
    public string CountryCode { get; private set; } = string.Empty;
    public virtual Country Country { get; internal set; } = null!;
    public virtual IReadOnlyList<VatNumberValidationRule> ValidationRules { get; private set; } = new List<VatNumberValidationRule>();

    public VatNumberDefinitionInfo ToDto()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        return World.Mapper.Map<VatNumberDefinitionInfo>(this);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
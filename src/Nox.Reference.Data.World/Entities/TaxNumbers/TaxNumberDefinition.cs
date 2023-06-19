namespace Nox.Reference;

public class TaxNumberDefinition : NoxReferenceEntityBase,
    IDtoConvertibleEntity<TaxNumberDefinitionInfo>
{
    public string LocalName { get; private set; } = string.Empty;
    public VerificationApi VerificationApi { get; private set; }
    public string CountryCode { get; private set; } = string.Empty;
    public virtual Country Country { get; internal set; } = null!;
    public virtual IReadOnlyList<TaxNumberValidationRule> ValidationRules { get; private set; } = new List<TaxNumberValidationRule>();

    public TaxNumberDefinitionInfo ToDto()
    {
        return World.Mapper.Map<TaxNumberDefinitionInfo>(this);
    }
}

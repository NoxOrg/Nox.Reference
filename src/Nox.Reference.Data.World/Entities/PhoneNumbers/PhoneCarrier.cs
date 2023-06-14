namespace Nox.Reference;

public class PhoneCarrier : NoxReferenceEntityBase,
    IDtoConvertibleEntity<PhoneCarrierInfo>
{
    public virtual IReadOnlyList<CarrierPhoneNumber> PhoneNumberCarriers { get; internal set; } = new List<CarrierPhoneNumber>();
    public string Name { get; private set; } = string.Empty;

    public PhoneCarrierInfo ToDto()
    {
        return World.Mapper.Map<PhoneCarrierInfo>(this);
    }
}
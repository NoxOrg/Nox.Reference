namespace Nox.Reference;

public class PhoneCarrier : NoxReferenceEntityBase,
    IDtoConvertibleEntity<PhoneCarrierInfo>
{
    public virtual IReadOnlyList<CarrierPhoneNumber> PhoneNumberCarriers { get; internal set; } = new List<CarrierPhoneNumber>();
    public string Name { get; private set; } = string.Empty;

    public PhoneCarrierInfo ToDto()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        return World.Mapper.Map<PhoneCarrierInfo>(this);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
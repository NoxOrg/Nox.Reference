namespace Nox.Reference;

public class CarrierPhoneNumber : NoxReferenceEntityBase
{
    public int PhoneNumber { get; internal set; }

    public virtual PhoneCarrier PhoneCarrier { get; private set; } = new PhoneCarrier();
}
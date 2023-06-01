using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CarrierPhoneNumber : WorldNoxReferenceEntity
{
    public int PhoneNumber { get; internal set; }

    public virtual PhoneCarrier PhoneCarrier { get; private set; } = new PhoneCarrier();
}
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CarrierPhoneNumber : INoxReferenceEntity
{
    public int Id { get; private set; }

    public int PhoneNumber { get; private set; }

    public PhoneCarrier PhoneCarrier { get; private set; } = new PhoneCarrier();
}
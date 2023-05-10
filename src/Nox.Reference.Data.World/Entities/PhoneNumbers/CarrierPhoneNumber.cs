using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CarrierPhoneNumber : INoxReferenceEntity
{
    public int Id { get; private set; }

    public int PhoneNumber { get; set; }

    public PhoneCarrier PhoneCarrier { get; set; } = new PhoneCarrier();
}
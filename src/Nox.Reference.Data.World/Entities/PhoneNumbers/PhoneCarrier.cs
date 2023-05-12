using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class PhoneCarrier : INoxReferenceEntity
{
    public int Id { get; private set; }
    public IReadOnlyList<CarrierPhoneNumber> PhoneNumberCarriers { get; internal set; } = new List<CarrierPhoneNumber>();
    public string Name { get; private set; } = string.Empty;
}
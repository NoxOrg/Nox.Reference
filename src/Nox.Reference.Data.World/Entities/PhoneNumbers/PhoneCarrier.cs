using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class PhoneCarrier : INoxReferenceEntity
{
    public int Id { get; private set; }
    public IReadOnlyList<CarrierPhoneNumber> PhoneNumberCarriers { get; set; } = new List<CarrierPhoneNumber>();
    public string Name { get; set; } = string.Empty;
}
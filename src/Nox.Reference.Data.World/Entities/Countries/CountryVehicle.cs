using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CountryVehicle : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string DrivingSide { get; set; } = string.Empty;
    public string InternationalRegistrationCodes { get; set; } = string.Empty;
}
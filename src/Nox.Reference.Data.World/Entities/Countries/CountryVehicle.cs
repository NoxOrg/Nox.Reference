using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CountryVehicle : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string DrivingSide { get; private set; } = string.Empty;
    public string InternationalRegistrationCodes { get; private set; } = string.Empty;
}
namespace Nox.Reference;

public class CountryVehicle : NoxReferenceEntityBase
{
    public string DrivingSide { get; private set; } = string.Empty;
    public string InternationalRegistrationCodes { get; private set; } = string.Empty;
}
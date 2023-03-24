namespace Nox.Reference.Countries;

public interface IVehicleInfo
{
    string DrivingSide { get; }
    IReadOnlyList<string> InternationalRegistrationCodes { get; }
}


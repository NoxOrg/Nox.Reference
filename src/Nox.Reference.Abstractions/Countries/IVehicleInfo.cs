namespace Nox.Reference.Abstractions.Countries;

public interface IVehicleInfo
{
    string DrivingSide { get; }
    IReadOnlyList<string> InternationalRegistrationCodes { get; }
}
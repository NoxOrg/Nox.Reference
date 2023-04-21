namespace Nox.Reference.Abstractions;

public interface IVehicleInfo
{
    string DrivingSide { get; }
    IReadOnlyList<string> InternationalRegistrationCodes { get; }
}
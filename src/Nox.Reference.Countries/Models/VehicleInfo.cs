
namespace Nox.Reference.Countries;

public class VehicleInfo : IVehicleInfo
{
    public string[] InternationalRegistrationCodes { get; set; } = Array.Empty<string>();

    public string DrivingSide { get; set; } = string.Empty;
}

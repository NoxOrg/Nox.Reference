
namespace Nox.Reference.Countries;

public class VehicleInfo : IVehicleInfo
{
    public IReadOnlyList<string> InternationalRegistrationCodes { get; set; } = new List<string>();

    public string DrivingSide { get; set; } = string.Empty;
}

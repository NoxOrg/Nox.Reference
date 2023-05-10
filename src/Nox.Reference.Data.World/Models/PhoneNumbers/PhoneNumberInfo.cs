using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

internal class PhoneNumberInfo : IPhoneNumberInfo
{
    public string InputNumber { get; set; } = string.Empty;
    public string FormattedNumber { get; set; } = string.Empty;
    public string FormattedNumberNational { get; set; } = string.Empty;
    public string FormattedNumberInternational { get; set; } = string.Empty;
    public string FormattedNumberRfc3966 { get; set; } = string.Empty;
    public bool IsValid { get; set; } = false;
    public bool IsMobile { get; set; } = false;
    public string NumberType { get; set; } = "UNKNOWN";
    public string RegionCode { get; set; } = string.Empty;
    public string RegionName { get; set; } = string.Empty;
    public string CarrierName { get; set; } = "Unknown";
}
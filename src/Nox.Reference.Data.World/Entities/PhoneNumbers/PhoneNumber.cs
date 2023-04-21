using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class PhoneNumber : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string InputNumber { get; set; } = string.Empty;
    public string FormattedNumber { get; set; } = string.Empty;
    public string FormattedNumberNational { get; set; } = string.Empty;
    public string FormattedNumberInternational { get; set; } = string.Empty;
    public string FormattedNumberRfc3966 { get; set; } = string.Empty;
    public bool IsValid { get; set; }
    public bool IsMobile { get; set; }
    public string NumberType { get; set; } = string.Empty;
    public string RegionCode { get; set; } = string.Empty;
    public string RegionName { get; set; } = string.Empty;
    public string CarrierName { get; set; } = string.Empty;
}
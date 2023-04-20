using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class PhoneNumber : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string InputNumber { get; set; }
    public string FormattedNumber { get; set; }
    public string FormattedNumberNational { get; set; }
    public string FormattedNumberInternational { get; set; }
    public string FormattedNumberRfc3966 { get; set; }
    public bool IsValid { get; set; }
    public bool IsMobile { get; set; }
    public string NumberType { get; set; }
    public string RegionCode { get; set; }
    public string RegionName { get; set; }
    public string CarrierName { get; set; }
}
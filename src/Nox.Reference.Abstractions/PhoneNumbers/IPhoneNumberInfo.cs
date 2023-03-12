namespace Nox.Reference.PhoneNumbers;

public interface IPhoneNumberInfo
{
    public string InputNumber { get; }
    public string FormattedNumber { get; }
    public string FormattedNumberNational { get; }
    public string FormattedNumberInternational { get; }
    public string FormattedNumberRfc3966 { get; }
    public bool IsValid { get; }
    public bool IsMobile { get; }
    public string NumberType { get; }
    public string RegionCode { get; }
    public string RegionName { get; }
    public string CarrierName { get; }
}

namespace Nox.Reference.Data.IpAddress;

public class IpAddress : NoxReferenceEntityBase
{
    public string CountryCode { get; set; } = string.Empty;
    public IpAddressChunk StartAddress { get; private set; } = new IpAddressChunk(default, default);
    public IpAddressChunk EndAddress { get; private set; } = new IpAddressChunk(default, default);
}
namespace Nox.Reference.Data.IpAddress;

internal class IpAddress : NoxReferenceEntityBase
{
    public string CountryCode { get; set; } = string.Empty;
    public IpAddressChunk StartAddress { get; set; } = new IpAddressChunk(default, default);
    public IpAddressChunk EndAddress { get; set; } = new IpAddressChunk(default, default);
}
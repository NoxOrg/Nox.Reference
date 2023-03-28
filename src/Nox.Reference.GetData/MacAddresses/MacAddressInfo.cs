using Nox.Reference.Abstractions.MacAddresses;

internal record MacAddressInfo : IMacAddressInfo
{
    public MacAddressInfo(string address, string vendor)
    {
        Address = address;
        Vendor = vendor;
    }

    public string Address { get; }
    public string Vendor { get; }
}
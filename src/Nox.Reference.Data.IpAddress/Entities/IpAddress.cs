using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.IpAddress;

public class IpAddress : NoxReferenceEntityBase
{
    public string CountryCode { get; private set; } = string.Empty;
    internal IpAddressChunk StartAddress { get; private set; } = new IpAddressChunk(default, default);
    //internal IpAddressChunk EndAddress { get; private set; } = new IpAddressChunk(default, default);
}

internal class IpAddressChunk
{
    private IpAddressChunk()
    { }

    public IpAddressChunk(ulong start, ulong end)
    {
        Start = start;
        End = end;
    }

    public ulong Start { get; }

    public ulong End { get; }

    public override string ToString()
    {
        return $"{Start}{End}";
    }
}
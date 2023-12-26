using System.Numerics;

namespace Nox.Reference.Data.IpAddress;

public class IpAddressChunk
{
    private IpAddressChunk()
    { }

    public IpAddressChunk(long start, long end)
    {
        Start = start;
        End = end;
    }

    public long Start { get; private set; }
    public long End { get; private set; }

    public static IpAddressChunk CreateIpAddressChunkFromNumber(BigInteger ipAddressNumber)
    {
        var startPart = (ulong)(ipAddressNumber >> 64);
        var endPart = (ulong)(ipAddressNumber & ulong.MaxValue);
        return new IpAddressChunk((long)startPart, (long)endPart);
    }

    public override string ToString()
    {
        return CombineLongsToBigInteger().ToString();
    }

    private BigInteger CombineLongsToBigInteger()
    {
        byte[] startBytes = BitConverter.GetBytes(Start);
        byte[] endBytes = BitConverter.GetBytes(End);

        byte[] combinedBytes = new byte[16];

        // Copy the bytes of long1 into the first 8 bytes of the combined array
        Buffer.BlockCopy(endBytes, 0, combinedBytes, 0, 8);

        // Copy the bytes of long2 into the last 8 bytes of the combined array
        Buffer.BlockCopy(startBytes, 0, combinedBytes, 8, 8);

        // Create a BigInteger from the combined bytes
        BigInteger combinedBigInt = new BigInteger(combinedBytes);

        return combinedBigInt;
    }
}
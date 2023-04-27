using System.IO.Compression;
using System.Text;

namespace Nox.Reference.Common;

public static class FileUtilities
{
    public static string DecompressGzip(string filePath)
    {
        using var compressedStream = new FileStream(filePath, FileMode.Open);
        var ms = new MemoryStream();

        using GZipStream decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress);
        decompressionStream.CopyTo(ms);

        return Encoding.UTF8.GetString(ms.ToArray());
    }
}
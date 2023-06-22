using System.IO.Compression;
using System.Text;

namespace Nox.Reference.Common;

internal static class FileUtilities
{
    /// <summary>
    /// This method converts .gzip file into string that
    /// can be deserialized
    /// </summary>
    /// <param name="filePath">Gzip file path</param>
    /// <returns>Deserialized string</returns>
    public static string DecompressGzip(string filePath)
    {
        using var compressedStream = new FileStream(filePath, FileMode.Open);
        var ms = new MemoryStream();

        using GZipStream decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress);
        decompressionStream.CopyTo(ms);

        return Encoding.UTF8.GetString(ms.ToArray());
    }
}
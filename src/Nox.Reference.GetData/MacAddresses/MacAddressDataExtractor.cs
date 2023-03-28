using System.Text.Json;
using Nox.Reference.Abstractions.MacAddresses;

internal static class MacAddressDataExtractor
{
    private const string SourceFilePath = @"MacAddresses\mac-vendor.txt";
    private const string OutputFilePath = "Nox.Reference.MacAddresses.json";

    public static void ExtractMacAddresses(string sourcePath, string outputPath)
    {
        using var sr = new StreamReader(Path.Combine(sourcePath, SourceFilePath));

        var arr = new List<IMacAddressInfo>();
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var data = line.Split('\t');

            var address = data[0].Trim();
            var vendor = string.Join(" ", data.Skip(1));

            arr.Add(new MacAddressInfo(address, vendor));
        }
        var serializedOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var jsonString = JsonSerializer.Serialize(arr, serializedOptions);

        using var sw = new StreamWriter(Path.Combine(outputPath, OutputFilePath));
        sw.WriteLine(jsonString);
    }
}
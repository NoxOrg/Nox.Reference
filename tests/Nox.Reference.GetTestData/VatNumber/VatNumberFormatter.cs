using Microsoft.VisualBasic.FileIO;
using System.Text.Json;

internal class VatNumberFormatter
{
    internal static void FormatVatNumberDataIntoFiles(string inputPath, string outputFilePath)
    {
        try
        {
            var sourcePath = Path.Combine(inputPath, "VatNumbers", "VatNumbersRaw.csv");
            if (!File.Exists(sourcePath))
            {
                Console.WriteLine($"Error! Source file not found by path: {sourcePath}. Finishing.");
                return;
            }

            using var parser = new TextFieldParser(sourcePath);
            var vatNumberByCountries = new Dictionary<string, List<string>>();
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                // Processing row
                // 0 -- VatNumber, 1 -- Country, 2 -- Index
                string[] fields = parser.ReadFields()!;
                
                if (!vatNumberByCountries.ContainsKey(fields[1]))
                {
                    vatNumberByCountries[fields[1]] = new List<string>();
                }

                vatNumberByCountries[fields[1]].Add(fields[0]);
            }

            outputFilePath = Path.Combine(outputFilePath, "VatNumbers");
            Directory.CreateDirectory(outputFilePath);
            var outputFilePostfix = "VatNumbers.json";

            foreach (var country in vatNumberByCountries)
            {
                // Store output
                var serializationOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                };

                var outputContent = JsonSerializer.Serialize(
                    country.Value,
                    serializationOptions);

                File.WriteAllText(Path.Combine($"{outputFilePath}", $"{country.Key}-{outputFilePostfix}"), outputContent);
            }

        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }
}
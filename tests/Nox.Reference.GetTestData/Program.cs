Console.WriteLine("Starting Nox.Reference Data collection...");
Console.WriteLine();

var path = new DirectoryInfo(Directory.GetCurrentDirectory());

while (!Directory.Exists(Path.Combine(path.FullName, ".git")))
{
    // not found, in root
    if (path == null || path.Parent == null)
    {
        path = new DirectoryInfo(Directory.GetCurrentDirectory());
        break;
    }
    path = path.Parent;
}

var targetOutputPath = Path.Combine(path.FullName, @"data");
Directory.CreateDirectory(targetOutputPath);

var sourceOutputPath = Path.Combine(path.FullName, @"data\source");
Directory.CreateDirectory(sourceOutputPath);

Console.WriteLine("Formatting VAT Number test data...");
VatNumberFormatter.FormatVatNumberDataIntoFiles(sourceOutputPath, Path.Combine(targetOutputPath, @"tests"));

Console.WriteLine();
Console.WriteLine("Completed.");

return 0;
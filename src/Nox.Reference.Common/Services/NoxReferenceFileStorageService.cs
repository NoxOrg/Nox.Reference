using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Nox.Reference.Common;

// TODO: make internal
public class NoxReferenceFileStorageService
{
    private readonly IConfiguration _configuration;

    public NoxReferenceFileStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SaveContentToSource(string content,
        string dataFolderRelativePath,
        string fileName)
    {
        var filePath = GetCreateFilePath(dataFolderRelativePath, fileName);
        File.WriteAllText(filePath, content);
    }

    public void SaveContentToSource(byte[] content,
       string dataFolderRelativePath,
       string fileName)
    {
        var filePath = GetCreateFilePath(dataFolderRelativePath, fileName);
        File.WriteAllBytes(filePath, content);
    }

    public void SaveDataToFile<TSource>(IEnumerable<TSource> data, string folderRelativePath, string fileName)
    {
        var targetPath = _configuration.GetValue<string>(ConfigurationConstants.TargetDataPathSettingName)!;
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var outputContent = JsonSerializer.Serialize(data, options);

        Directory.CreateDirectory(Path.Combine(targetPath, folderRelativePath));

        File.WriteAllText(Path.Combine(targetPath, folderRelativePath, fileName), outputContent, Encoding.Unicode);
    }

    public string GetFileContentFromSource(
        string folderName,
        string fileName)
    {
        var sourcePath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var filePath = Path.Combine(sourcePath, folderName, fileName);

        return File.ReadAllText(filePath);
    }

    private string GetCreateFilePath(string dataFolderRelativePath, string fileName)
    {
        var sourcePath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var filePath = Path.Combine(sourcePath, dataFolderRelativePath, fileName);

        Directory.CreateDirectory(Path.Combine(sourcePath, dataFolderRelativePath));
        return filePath;
    }
}
using System.Text;

namespace Nox.Reference.Common;

/// <summary>
/// This service generates custom enums for fetched data
/// that allows easier and better-readable entity access
/// </summary>
public static class EnumGeneratorService
{
    public static void Generate<TEntity>(
        IEnumerable<TEntity> entities,
        Func<TEntity, string> nameGetter,
        string enumNameSpace,
        string enumName)
    {
        var sb = new StringBuilder($"namespace Nox.Reference.Data.{enumNameSpace};\n");

        sb.AppendLine($"public enum {enumName}");
        sb.AppendLine("{");
        foreach (var entity in entities)
        {
            var itemKey = nameGetter(entity);
            if (string.IsNullOrEmpty(itemKey))
            {
                continue;
            }

            sb.AppendLine($"\t{nameGetter(entity)},");
        }
        sb.Remove(sb.Length - 1, 1);
        sb.AppendLine("}");
        sb.Append("\r\n");
        var filePath = ResolveFilePath(enumNameSpace, enumName);
        File.WriteAllText(filePath, sb.ToString().Trim());
    }

    private static string ResolveFilePath(string enumNameSpace, string enumName)
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        var path = Path.Combine(directory!.FullName, $"Nox.Reference.Data.{enumNameSpace}", "Generated");
        Directory.CreateDirectory(path);
        return Path.Combine(path, $"{enumName}.cs");
    }
}
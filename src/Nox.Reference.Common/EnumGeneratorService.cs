﻿using System.Text;

namespace Nox.Reference.Common;

public static class EnumGeneratorService
{
    public static void Generate<TEntity>(
        IEnumerable<TEntity> entities,
        Func<TEntity, string> nameGetter,
        string enumNameSpace,
        string enumName)
    {
        var sb = new StringBuilder($"namespace Nox.Reference.Data.{enumNameSpace};\n");

        sb.AppendLine($"public enum {enumName}\n");
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
        sb.AppendLine("}");
        var filePath = ResolveFilePath(enumNameSpace, enumName);
        File.WriteAllText(filePath, sb.ToString());
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
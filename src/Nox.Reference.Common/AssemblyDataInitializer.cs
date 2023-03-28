using System.Reflection;
using System.Text.Json;

namespace Nox.Reference.Common;

public static class AssemblyDataInitializer
{
    public static IEnumerable<TType> GetDataFromAssemblyResource<TType>(string resourceName)
    {
        var assembly = Assembly.GetCallingAssembly();

        if (assembly == null)
        {
            throw new InvalidOperationException("ExecutingAssembly was not found");
        }

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new InvalidOperationException($"Resource {resourceName} stream is null or empty");
        }

        using var reader = new StreamReader(stream);

        var jsonContent = reader.ReadToEnd();

        var data = JsonSerializer.Deserialize<TType[]>(jsonContent);

        if (data == null || !data.Any())
        {
            throw new InvalidOperationException($"Deserialized in {resourceName} collection is null or empty.");
        }

        return data;
    }
}
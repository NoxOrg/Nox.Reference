using System.Reflection;
using System.Text.Json;

namespace Nox.Reference.Common;

public static class AssemblyDataInitializer
{
    public static IEnumerable<TType> GetDataFromAssemblyResource<TType>(string resourceName)
    {
        var assembly = Assembly.GetCallingAssembly()!;

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new NoxDataExtractorException($"Resource {resourceName} stream is null or empty");
        }

        using var reader = new StreamReader(stream);

        var jsonContent = reader.ReadToEnd();

        var data = JsonSerializer.Deserialize<TType[]>(jsonContent);

        if (data == null || !data.Any())
        {
            throw new NoxDataExtractorException($"Deserialized collection from {resourceName} is null or empty.");
        }

        return data;
    }
}
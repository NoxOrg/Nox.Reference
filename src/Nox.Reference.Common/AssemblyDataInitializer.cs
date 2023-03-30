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
            throw new NoxDataExtractorException($"CallingAssembly was not found");
        }

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new NoxDataExtractorException($"Resource {resourceName} was not found in assesmbly {assembly.FullName}");
        }

        using var reader = new StreamReader(stream);
        var jsonContent = reader.ReadToEnd();

        var data = JsonSerializer.Deserialize<TType[]>(jsonContent);

        if (data == null || !data.Any())
        {
            throw new NoxDataExtractorException($"Reference data for {resourceName} is unexpectedly missing from assembly {assembly.FullName}.");
        }

        return data;
    }
}
using System.Reflection;

namespace Nox.Reference.Common;

public abstract class AssemblyDataInitializer<TType>
{
    public IEnumerable<TType> GetDataFromAssemblyResource()
    {
        var assembly = Assembly.GetCallingAssembly();

        if (assembly == null)
        {
            throw new InvalidOperationException("ExecutingAssembly was not found");
        }

        using var stream = assembly.GetManifestResourceStream(ResourceName);
        if (stream == null)
        {
            throw new InvalidOperationException("Assembly stream is null or empty");
        }

        using var reader = new StreamReader(stream);

        var content = reader.ReadToEnd();

        return CreateCollectionDataFromContent(content);
    }

    protected abstract string ResourceName { get; }

    protected abstract IEnumerable<TType> CreateCollectionDataFromContent(string content);
}
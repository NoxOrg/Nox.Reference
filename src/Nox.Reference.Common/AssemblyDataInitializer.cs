﻿using System.Reflection;
using Newtonsoft.Json;

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
            throw new InvalidOperationException("Assembly stream is null or empty");
        }

        using var reader = new StreamReader(stream);

        var jsonContent = reader.ReadToEnd();

        var data = JsonConvert.DeserializeObject<TType[]>(jsonContent);

        if (data == null || !data.Any())
        {
            throw new InvalidOperationException("Deserialized collection is null or empty.");
        }

        return data;
    }
}
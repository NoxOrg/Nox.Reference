using System.Reflection;
using AutoMapper;

namespace Nox.Reference.Common;

public static class MapperHolder
{
    private static readonly List<Assembly> _assemblyList = new List<Assembly>();

    internal static void AddMapper(Assembly assembly)
    {
        _assemblyList.Add(assembly);
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(_assemblyList);
        });

        Mapper = config.CreateMapper();
    }

    public static IMapper Mapper { get; private set; } = new MapperConfiguration(cfg => { }).CreateMapper();
}
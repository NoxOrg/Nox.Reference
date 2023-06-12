using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;

[assembly: InternalsVisibleTo("Nox.Reference.Data.Common")]

namespace Nox.Reference.Common;

internal static class MapperHolder
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
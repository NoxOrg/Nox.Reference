using AutoMapper;

namespace Nox.Reference.Data.World.Extensions;

public static class AutomapperExtensions
{
    public static IMappingExpression<TSource, TDestination> IgnoreAllMembers<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expr)
    {
        var destinationType = typeof(TDestination);

        foreach (var property in destinationType.GetProperties())
            expr.ForMember(property.Name, opt => opt.Ignore());

        return expr;
    }
}
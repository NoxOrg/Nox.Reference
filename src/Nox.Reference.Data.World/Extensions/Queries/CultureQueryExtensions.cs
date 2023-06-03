using System.Reflection.Metadata;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CultureQueryExtensions
{
    /// <summary>
    /// This method returns culture info by culture name
    /// <example>
    /// <code>
    /// Cultures.Get("uz-Arab")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="code">Culture name. Example: "uz-Arab".</param>
    /// <returns>Culture info</returns>
    public static Culture? Get(this IQueryable<Culture> query, string code)
    {
        return query.GetByName(code);
    }

    /// <summary>
    /// This method returns culture info by culture name
    /// <example>
    /// <code>
    /// Cultures.GetByName("uz-Arab")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="code">Culture name. Example: "uz-Arab".</param>
    /// <returns>Culture info</returns>
    public static Culture? GetByName(this IQueryable<Culture> query, string code)
    {
        return query.FirstOrDefault(x => x.Name.Equals(code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// This method returns culture info by culture formal name
    /// <example>
    /// <code>
    /// Cultures.GetByFormalName("Uzbek (Arabic)")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="formalName">Culture name. Example: "Uzbek (Arabic)".</param>
    /// <returns>Culture info</returns>
    public static Culture? GetByFormalName(this IQueryable<Culture> query, string formalName)
    {
        return query.FirstOrDefault(x => x.FormalName.Equals(formalName, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// This method returns culture info by culture native name
    /// <example>
    /// <code>
    /// Cultures.GetByNativeName("(اوزبیک(عربی")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="nativeName">Culture native name. Example: "(اوزبیک(عربی".</param>
    /// <returns>Culture info</returns>
    public static Culture? GetByNativeName(this IQueryable<Culture> query, string nativeName)
    {
        return query.FirstOrDefault(x => x.NativeName.Equals(nativeName, StringComparison.OrdinalIgnoreCase));
    }
}
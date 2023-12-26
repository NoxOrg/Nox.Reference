using System.Reflection;

namespace Nox.Reference.Data.Common.Helpers;

/// <summary>
/// This class contains helper functions that are needed to fix database path
/// </summary>
internal static class DatabasePathHelper
{
    private static readonly char FileSeparator = Path.DirectorySeparatorChar;

    /// <summary>
    /// This method is used to fix database file detection. For different types of applications
    /// Directory.GetCurrentDirectory returns different values, which leads to file not found
    /// errors. This method checks if the default relative path is working and fixes it according to
    /// assembly location.
    /// </summary>
    /// <param name="connectionString">Provided connection string to check</param>
    /// <param name="databaseContextType">Database context type</param>
    /// <param name="contextName">Name of the current database context</param>
    /// <returns>Fixed connection string</returns>
    /// <exception cref="ArgumentNullException">Provided parameter is null or whitespace</exception>
    /// <exception cref="Exception"></exception>
    public static string FixConnectionStringPathUsingAssemblyPath(string? connectionString, Type databaseContextType, string contextName)
    {
        ThrowIfConnectionStringIsNotDefined(connectionString, contextName);

        if (CheckIfFileInConnectionStringExists(connectionString))
            return connectionString!;

        return GetFixedConnectionString(connectionString, databaseContextType, contextName);
    }

    private static void ThrowIfConnectionStringIsNotDefined(string? connectionString, string contextName)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            var errorMessage = $"{contextName} database file path is null";
            throw new ArgumentNullException(errorMessage);
        }
    }

    private static bool CheckIfFileInConnectionStringExists(string? connectionString)
    {
        var filePath = connectionString!.Split("=")[1];
        return File.Exists(filePath);
    }

    private static string GetFixedConnectionString(string? connectionString, Type databaseContextType, string contextName)
    {
        var currentAssemblyLocation = Assembly.GetAssembly(databaseContextType)!.Location;
        var currentAssemblyDirectory = Path.GetDirectoryName(currentAssemblyLocation);

        var fixedConnectionString = Path.Combine(connectionString!.Split(separator: '\\', '/'));

        // resolve . operator
        fixedConnectionString = fixedConnectionString!.Replace($"=.{FileSeparator}", $"={currentAssemblyDirectory}{FileSeparator}");

        // resolve .. operator
        fixedConnectionString = fixedConnectionString.Replace($"=..{FileSeparator}", $"={currentAssemblyDirectory}{FileSeparator}..{FileSeparator}");

        var filePath = fixedConnectionString.Split("=")[1];
        ThrowIfFileInConnectionStringDoesNotExist(connectionString, contextName, filePath);

        return fixedConnectionString;
    }

    private static void ThrowIfFileInConnectionStringDoesNotExist(string? connectionString, string contextName, string filePath)
    {
        if (!File.Exists(filePath))
        {
            var errorMessage = $"Cannot find {contextName} database by the provided connection string {connectionString}. Please, check the path or override it using {contextName}.UseDatabasePath(path). Possibly an issue with '.\\' operator is present.";
            throw new FileNotFoundException(errorMessage);
        }
    }
}
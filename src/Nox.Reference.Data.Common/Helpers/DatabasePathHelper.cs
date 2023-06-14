using System.Reflection;

namespace Nox.Reference.Data.Common.Helpers
{
    /// <summary>
    /// This class contains helper functions that are needed to fix database path
    /// </summary>
    // TODO: make internal
    public static class DatabasePathHelper
    {
        /// <summary>
        /// This method is used to fix database file detection. For different types of applications
        /// Directory.GetCurrentDirectory returns different values, which leads to file not found errors.
        /// This method checks if the default relative path is working and fixes it according to assembly location
        /// </summary>
        /// <param name="connectionString">Provided connection string to check</param>
        /// <param name="databaseContextType">Database context type</param>
        /// <param name="contextName">Name of the current database context</param>
        /// <returns>Fixed connection string</returns>
        /// <exception cref="ArgumentNullException">Provided parameter is null or whitespace</exception>
        /// <exception cref="Exception"></exception>
        public static string FixConnectionStringPathUsingAssemblyPath(string? connectionString, Type databaseContextType, string contextName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var errorMessage = $"{contextName} database file path is null";
                throw new ArgumentNullException(errorMessage);
            }
            var splitDbPath = connectionString.Split("=");
            var filePath = splitDbPath[1];

            if (File.Exists(filePath))
            {
                return connectionString;
            }

            var currentAssemblyLocation = Assembly.GetAssembly(databaseContextType)!.Location;
            var currentAssemblyDirectory = Path.GetDirectoryName(currentAssemblyLocation);

            // resolve . operator
            var fixedConnectionString = connectionString.Replace("=.\\", $"={currentAssemblyDirectory}\\");

            // resolve .. operator
            fixedConnectionString = fixedConnectionString.Replace("=..\\", $"={currentAssemblyDirectory}\\..\\");

            filePath = fixedConnectionString.Split("=")[1];
            if (!File.Exists(filePath))
            {
                var errorMessage = $"Cannot find {contextName} database by the provided connection string {connectionString}. Please, check the path or override it using {contextName}.UseDatabasePath(path). Possibly an issue with '.\\' operator is present.";
                // TODO change exception type.
                throw new Exception(errorMessage);
            }

            return fixedConnectionString;
        }
    }
}

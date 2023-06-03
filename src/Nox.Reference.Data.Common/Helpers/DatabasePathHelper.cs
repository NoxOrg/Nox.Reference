using System.Reflection;

namespace Nox.Reference.Data.Common.Helpers
{
    public static class DatabasePathHelper
    {
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

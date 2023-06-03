using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Helpers;

namespace Nox.Reference.Data.Machine;

public class MachineDbContext : DbContext, IMachineInfoContext
{
    private readonly IConfiguration _configuration;
    private static string? _databasePath;

    // DON'T remove default constructor. It is used for migrations purposes.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MachineDbContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public MachineDbContext(
        DbContextOptions<MachineDbContext> options,
        IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public IQueryable<MacAddress> MacAddresses
         => Set<MacAddress>()
            .AsNoTracking()
            .AsQueryable();

    /// <summary>
    /// <para>Override default database path. Examples: </para>
    /// <para>'Data Source=.\NoxReferenceDatabase\Nox.Reference.Machine.db'</para>
    /// <para>'Data Source=..\..\data\Nox.Reference.Machine.db'</para>
    /// <para>'Data Source=C:\project\NoxReferenceDatabase\Nox.Reference.Machine.db'</para>
    /// </summary>
    /// <param name="databasePath">New overridden database connection string</param>
    public static void UseDatabaseConnectionString(string databasePath)
    {
        _databasePath = databasePath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = _databasePath ?? _configuration.GetConnectionString(ConfigurationConstants.MachineConnectionStringName);
        connectionString = DatabasePathHelper.FixConnectionStringPathUsingAssemblyPath(connectionString, typeof(MachineDbContext), nameof(Machine));
        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MacAddressConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
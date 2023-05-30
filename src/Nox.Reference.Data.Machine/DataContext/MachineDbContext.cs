using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Common;

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

    public static void UseDatabasePath(string databasePath)
    {
        _databasePath = databasePath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = _databasePath ?? _configuration.GetConnectionString(ConfigurationConstants.MachineConnectionStringName);
        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MacAddressConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Helpers;
using Nox.Reference.Data.IpAddress.Configurations;

namespace Nox.Reference.Data.IpAddress;

public class IpAddressDbContext : DbContext, IIpAddressInfoContext
{
    private readonly IConfiguration _configuration;
    private static string? _databasePath;

    // DON'T remove default constructor. It is used for migrations purposes.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public IpAddressDbContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public IpAddressDbContext(
        DbContextOptions<IpAddressDbContext> options,
        IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public IQueryable<IpAddress> IpAddresses
         => Set<IpAddress>()
            .AsNoTracking()
            .AsQueryable();

    /// <summary>
    /// <para>Override default database path. Examples: </para>
    /// <para>'Data Source=.\NoxReferenceDatabase\Nox.Reference.IpAddress.db'</para>
    /// <para>'Data Source=..\..\data\Nox.Reference.IpAddress.db'</para>
    /// <para>'Data Source=.\..\NoxReferenceDatabase\Nox.Reference.IpAddress.db'</para>
    /// </summary>
    /// <param name="databasePath">New overridden database connection string</param>
    public static void UseDatabaseConnectionString(string databasePath)
    {
        _databasePath = databasePath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = _databasePath ?? _configuration.GetConnectionString(ConfigurationConstants.IpAddressConnectionStringName);

        // TODO: fix adding migrations. Currently throws an error of "empty db path". Need to find a way of fixing it.
        //connectionString = DatabasePathHelper.FixConnectionStringPathUsingAssemblyPath(connectionString, typeof(IpAddressDbContext), nameof(IpAddressContext));

        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new IpAddressConfiguration());

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IpAddress>().OwnsOne(x => x.StartAddress);
    }
}
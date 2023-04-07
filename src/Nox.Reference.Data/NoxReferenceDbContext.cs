using Microsoft.EntityFrameworkCore;
using Nox.Reference.Data.Configurations;
using Nox.Reference.Entity;

namespace Nox.Reference.Data;

public class NoxReferenceDbContext : DbContext
{
    public DbSet<MacAddress> MacAddresses { get; set; }

    public NoxReferenceDbContext(DbContextOptions<NoxReferenceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MacAddressConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
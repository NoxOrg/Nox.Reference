using Microsoft.EntityFrameworkCore;
using Nox.Reference.Common;
using Nox.Reference.Entity;

namespace Nox.Reference.Data;

public class NoxReferenceDbContext : DbContext
{
    public DbSet<MacAddress> MacAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MacAddressConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}

public class NoxReferenceDataSeed : INoxReferenceSeed<MacAddress>
{
    private readonly NoxReferenceDbContext _dbContext;

    public NoxReferenceDataSeed(NoxReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed(IEnumerable<MacAddress> data)
    {
        foreach (var macAddress in data)
        {
            _dbContext.Set<MacAddress>().Add(macAddress);
        }
    }
}
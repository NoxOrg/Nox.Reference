using Microsoft.EntityFrameworkCore;

namespace Nox.Reference.Data.Common;

public class DatabaseMigrator<TDbContext> : INoxReferenceDatabaseMigrator
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    protected DatabaseMigrator(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Migrate()
    {
        _dbContext.Database.Migrate();
    }
}
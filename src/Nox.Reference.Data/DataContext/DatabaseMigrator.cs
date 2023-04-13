using Microsoft.EntityFrameworkCore;

namespace Nox.Reference.Data;

internal class DatabaseMigrator : INoxReferenceDatabaseMigrator
{
    private readonly NoxReferenceDbContext _dbContext;

    public DatabaseMigrator(NoxReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Migrate()
    {
        _dbContext.Database.Migrate();
    }
}
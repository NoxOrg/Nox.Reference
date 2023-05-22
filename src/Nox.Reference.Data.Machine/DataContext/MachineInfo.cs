using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Common;
using Nox.Reference.Data.Machine;

namespace Nox.Reference.Data;

public static class MachineInfo
{
    private static readonly IMachineInfoContext _dbContext;

#pragma warning disable S3963 // "static" fields should be initialized inline

    static MachineInfo()
#pragma warning restore S3963 // "static" fields should be initialized inline
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigurationConstants.ConfigFileName)
            .Build();
        _dbContext = new MachineDbContext(new DbContextOptions<MachineDbContext>(), configuration);
    }

    public static IQueryable<MacAddress> MacAddresses
        => _dbContext.MacAddresses;
}
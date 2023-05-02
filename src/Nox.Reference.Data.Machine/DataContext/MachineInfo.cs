using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using Nox.Reference.Data.Machine;
using System.Reflection;

namespace Nox.Reference.Data;

public static class MachineInfo
{
    private static readonly IMachineContext _dbContext;

#pragma warning disable S3963 // "static" fields should be initialized inline

    static MachineInfo()
#pragma warning restore S3963 // "static" fields should be initialized inline
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });
        var mapper = mapperConfiguration.CreateMapper();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigurationConstants.ConfigFileName)
            .Build();
        _dbContext = new MachineDbContext(new DbContextOptions<MachineDbContext>(), mapper, configuration);
    }

    public static IQueryable<IMacAddressInfo> MacAddresses
        => _dbContext.MacAddresses;
}
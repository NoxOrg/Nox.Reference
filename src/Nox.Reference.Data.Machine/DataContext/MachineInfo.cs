using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions;
using Nox.Reference.Data.Machine;

namespace Nox.Reference.Data;

public static class MachineInfo
{
    private static readonly IMachineContext _dbContext;

    static MachineInfo()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });
        var mapper = mapperConfiguration.CreateMapper();
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsetings.json")
            .Build();
        _dbContext = new MachineDbContext(new DbContextOptions<MachineDbContext>(), mapper, configuration);
    }

    public static IQueryable<IMacAddressInfo> MacAddresses
        => _dbContext.MacAddresses;
}
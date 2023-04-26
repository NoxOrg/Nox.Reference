using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions;
using Nox.Reference.Common;

namespace Nox.Reference.Data.Machine;

internal class MachineDbContext : DbContext, IMachineContext
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    // DON'T remove default constructor. It is used for migrations purposes.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MachineDbContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public MachineDbContext(
        DbContextOptions<MachineDbContext> options,
        IMapper mapper,
        IConfiguration configuration)
        : base(options)
    {
        _mapper = mapper;
        _configuration = configuration;
    }

    public IQueryable<IMacAddressInfo> MacAddresses
         => Set<MacAddress>()
            .AsQueryable()
            .ProjectTo<MacAddressInfo>(_mapper.ConfigurationProvider);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = _configuration.GetConnectionString(ConfigurationConstants.MachineConnectionStringName);
        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MacAddressConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
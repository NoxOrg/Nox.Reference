using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Common;

namespace Nox.Reference.MacAddresses.DataContext;

internal class MacAddressDbContext : DbContext, IMacAddressContext
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    // DON'T remove default constructor. It is used for migrations purposes.
    public MacAddressDbContext()
    {
    }

    public MacAddressDbContext(
        IMapper mapper,
        IConfiguration configuration,
        DbContextOptions<MacAddressDbContext> options)
        : base(options)
    {
        _mapper = mapper;
        _configuration = configuration;
    }

    public IQueryable<IMacAddressInfo> MacAddresses
         => Set<MacAddress>()
            .AsQueryable()
            .ProjectTo<IMacAddressInfo>(_mapper.ConfigurationProvider);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = _configuration.GetConnectionString(ConfigurationConstants.ConnectionStringName);
        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MacAddressConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
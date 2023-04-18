using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses.DataContext;

internal class MacAddressDbContext : DbContext, IMacAddressContext
{
    private readonly Mapper _mapper;

    public MacAddressDbContext(DbContextOptions<MacAddressDbContext> options, Mapper mapper)
        : base(options)
    {
        _mapper = mapper;
    }

    IQueryable<IMacAddressInfo> IMacAddressContext.MacAddresses
        => Set<MacAddress>()
            .AsQueryable()
            .ProjectTo<IMacAddressInfo>(_mapper.ConfigurationProvider);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MacAddressConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
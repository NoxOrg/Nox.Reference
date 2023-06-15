using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nox.Reference.Common;
using Nox.Reference.Data;
using Nox.Reference.Data.IpAddress;
using Nox.Reference.Data.Machine;
using Nox.Reference.Data.World;
using Nox.Reference.DataLoaders;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        var config = services.GetNoxReferenceConfiguration();

        WorldDbContext.UseDatabaseConnectionString(config.GetConnectionString(ConfigurationConstants.WorldDataLoadConnectionStringName)!);
        MachineDbContext.UseDatabaseConnectionString(config.GetConnectionString(ConfigurationConstants.MachineDataLoadConnectionStringName)!);
        IpAddressDbContext.UseDatabaseConnectionString(config.GetConnectionString(ConfigurationConstants.IpAddressConnectionStringName)!);

        services.AddScoped<DataSeedRunner>();
        //services.AddMachineContext();
        //services.AddWorldContext();
        services.AddIpAddressContext();
    })
    .ConfigureLogging(x => x.Services.AddLogging())
    .Build();

var dataSeedRunner = host.Services.GetRequiredService<DataSeedRunner>();
dataSeedRunner.Run();
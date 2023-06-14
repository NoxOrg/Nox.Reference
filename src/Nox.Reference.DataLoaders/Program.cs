﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nox.Reference;
using Nox.Reference.Data.World;
using Nox.Reference.DataLoaders;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        var config = services.GetNoxReferenceConfiguration();

        WorldDbContext.UseDatabaseConnectionString(config.GetConnectionString(ConfigurationConstants.WorldDataLoadConnectionStringName)!);
        MachineDbContext.UseDatabaseConnectionString(config.GetConnectionString(ConfigurationConstants.MachineDataLoadConnectionStringName)!);

        services.AddScoped<DataSeedRunner>();
        services.AddMachineContext();
        services.AddWorldContext();
    })
    .ConfigureLogging(x => x.Services.AddLogging())
    .Build();

var dataSeedRunner = host.Services.GetRequiredService<DataSeedRunner>();
dataSeedRunner.Run();
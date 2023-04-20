using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nox.Refeence.DataLoaders;
using Nox.Reference.Data;
using Nox.Reference.Data.Machine;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddScoped<DataSeedRunner>();
        services.AddMachineContext();
        services.AddWorldContext();
    })
    .Build();

var dataSeedRunner = host.Services.GetRequiredService<DataSeedRunner>();
dataSeedRunner.Run();
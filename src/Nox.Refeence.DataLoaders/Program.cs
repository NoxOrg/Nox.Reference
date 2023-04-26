using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nox.Reference.Data;
using Nox.Reference.DataLoaders;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddScoped<DataSeedRunner>();
        services.AddMachineContext();
        services.AddWorldContext();
    })
    .ConfigureLogging(x => x.Services.AddLogging())
    .Build();

var dataSeedRunner = host.Services.GetRequiredService<DataSeedRunner>();
dataSeedRunner.Run();
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nox.Refeence.DataLoaders;
using Nox.Reference.Data.Extensions;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddScoped<DataSeedRunner>();
        services.AddMacAddressDbContext();
    })
    .Build();

var dataSeedRunner = host.Services.GetRequiredService<DataSeedRunner>();
dataSeedRunner.Run();
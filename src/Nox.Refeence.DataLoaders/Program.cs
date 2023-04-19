using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nox.Refeence.DataLoaders;
using Nox.Reference.Country.DataContext;
using Nox.Reference.MacAddress.DataContext;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddScoped<DataSeedRunner>();
        services.AddMacAddressDbContext();
        services.AddCountryDbContext();
    })
    .Build();

var dataSeedRunner = host.Services.GetRequiredService<DataSeedRunner>();
dataSeedRunner.Run();
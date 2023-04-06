using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nox.Reference.GetData.CliCommands;
using Nox.Reference.GetData.CliCommands.MacAddresses;

var host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(configurationBuilder =>
    {
        configurationBuilder.AddJsonFile("appSettings.json");
    })
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddSingleton<ICliCommandExecutor, CliCommandExecutor>();

        services.AddScoped<CountryDataExtractCommand>();
        services.AddScoped<CurrencyDataExtractCommand>();
        services.AddScoped<MacAddressDataSeed>();
    })
    .Build();

var commandExecutor = host
    .Services
    .GetRequiredService<ICliCommandExecutor>();

commandExecutor.Run();
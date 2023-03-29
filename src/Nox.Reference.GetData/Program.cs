using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nox.Reference.GetData.CliCommands;
using Nox.Reference.MacAddresses;

var host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(configurationBuilder =>
    {
        configurationBuilder.AddJsonFile("appSettings.json");
    })
    .ConfigureServices(services =>
    {
        services.AddLogging();

        services.AddNoxMacAddresses();
        services.AddSingleton<ICliCommandExecutor, CliCommandExecutor>();

        services.AddScoped<EnviromentSetupCommand>();
        services.AddScoped<CountryDataExtractCommand>();
        services.AddScoped<CurrencyDataExtractCommand>();
        services.AddScoped<MacAddressDataExtractCommand>();
    })
    .Build();

var commandExecutor = host
    .Services
    .GetRequiredService<ICliCommandExecutor>();

commandExecutor.Run();
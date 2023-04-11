using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nox.Reference.Data;
using Nox.Reference.Data.Seeds;
using Nox.Reference.GetData;
using Nox.Reference.GetData.DataSeeds;
using Nox.Reference.GetData.DataSeeds.MacAddresses;

var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .AddJsonFile("appsettings.json")
        .Build();

var host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(builder =>
    {
        builder.Sources.Clear();
        builder.AddConfiguration(configuration);
    })
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddScoped<IDataSeederExecutor, DataSeederExecutor>();

        services.AddNoxReferenceData(configuration);

        //services.AddScoped<CountryDataSeeder>();
        services.AddScoped<CurrencyDataSeeder>();
        services.AddScoped<MacAddressDataSeeder>();
    })
    .Build();

var commandExecutor = host
    .Services
    .GetRequiredService<IDataSeederExecutor>();

var context = host.Services.GetRequiredService<NoxReferenceDbContext>();

context.Database.Migrate();

commandExecutor.Run();
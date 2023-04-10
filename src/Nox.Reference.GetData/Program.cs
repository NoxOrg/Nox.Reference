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

var host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(configurationBuilder =>
    {
        configurationBuilder.AddJsonFile("appSettings.json");
    })
    .ConfigureServices(services =>
    {
        services.AddLogging();
        // services.AddScoped<IDataSeederExecutor, DataSeederExecutor>();

        services.AddNoxReferenceData();

        //services.AddScoped<CountryDataSeeder>();
        //services.AddScoped<CurrencyDataSeeder>();
        //services.AddScoped<MacAddressDataSeeder>();
    })
    .Build();

var commandExecutor = host
    .Services
    .GetRequiredService<IDataSeederExecutor>();

var context = host.Services.GetRequiredService<NoxReferenceDbContext>();
//context.Database.Migrate();

commandExecutor.Run();
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nox.Reference;
using Nox.Reference.Data;
using Nox.Reference.Data.Seeds;
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
        services.AddScoped<ICliCommandExecutor, CliCommandExecutor>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddDbContext<NoxReferenceDbContext>(options =>
        {
            options.UseSqlite("Data Source=noxreferences.db;Version=3;");
        });

        services.AddScoped<CountrySeed>();
        services.AddScoped<CurrencyDataExtractCommand>();
        services.AddScoped<MacAddressDataSeed>();
        services.AddScoped(typeof(INoxReferenceSeed<>), typeof(NoxReferenceDatabaseSeed<>));
    })
    .Build();

var commandExecutor = host
    .Services
    .GetRequiredService<ICliCommandExecutor>();

var context = host.Services.GetRequiredService<NoxReferenceDbContext>();
context.Database.Migrate();

commandExecutor.Run();
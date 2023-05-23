using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data;
using Nox.Reference.Data.Machine;
using Nox.Reference.Data.World;
using Nox.Reference.Data.World.Extensions.Queries;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("This is Nox.Reference Demo!");

// Simplified

var worldContextInline = WorldDataContext.Create();
var timezone = worldContextInline.TimeZones.Get("EET")!;

Console.WriteLine($"Inline -- TimeZone -- {timezone.Id} -- {timezone.Type}");

var machineContextInline = MachineDataContext.Create();
var macAddress = machineContextInline.MacAddresses.Get("00-16-F6-11-22-33")!;

Console.WriteLine($"Inline -- MacAddress -- {macAddress.Id} -- {macAddress.OrganizationName}");

var country = worldContextInline.Countries
    .Include(x => x.NameTranslations)
    .ThenInclude(x => x.Language)
    .Get("ZAF")!;
var translation = country.GetTranslation("en")!;

Console.WriteLine($"Inline -- Translation -- {translation.Id} -- {translation.CommonName}");

var countryWithoutInclude = worldContextInline.Countries
    .Get("ZAF")!;
var translationWithoutInclude = countryWithoutInclude.GetTranslation("en")!;

Console.WriteLine($"Inline -- TranslationWithoutInclude -- {translationWithoutInclude?.Id} -- {translationWithoutInclude?.CommonName}");

// From DI

var serviceCollection = new ServiceCollection();
serviceCollection.AddWorldContext();
serviceCollection.AddMachineContext();

var serviceProvider = serviceCollection.BuildServiceProvider();

var worldContextDi = serviceProvider.GetRequiredService<IWorldInfoContext>();
timezone = worldContextDi.TimeZones.Get("EET")!;

Console.WriteLine($"DI -- TimeZone -- {timezone.Id} -- {timezone.Type}");

var machineContextDi = serviceProvider.GetRequiredService<IMachineInfoContext>();
macAddress = machineContextDi.MacAddresses.Get("00-16-F6-11-22-33")!;

Console.WriteLine($"DI -- MacAddress -- {macAddress.Id} -- {macAddress.OrganizationName}");
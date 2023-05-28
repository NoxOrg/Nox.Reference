using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data;
using Nox.Reference.Data.Machine;
using Nox.Reference.Data.World;
using Nox.Reference.Data.World.Extensions.Queries;

Console.WriteLine("This is Nox.Reference Demo!");

// Simplified flow

// World
// Country
var ukraine1 = World.Countries.Get("UKR");
var ukraine2 = World.Countries.First(x => x.FipsCode == "UP");
var ukraine3 = World.Countries.GetByAlpha2Code("UA");

Console.WriteLine($"Inline -- Country -- {ukraine1!.Id} -- {ukraine1.Names.CommonName}");
Console.WriteLine($"Inline -- Country -- {ukraine2.FipsCode} -- {ukraine1.Names.CommonName}");
Console.WriteLine($"Inline -- Country -- {ukraine3!.AlphaCode2} -- {ukraine1.Names.CommonName}");

var countryEnglishTranslation = World.Countries.Get("ZAF")!.NameTranslations.FirstOrDefault(x => x.Language.Iso_639_1 == "cs")!;
Console.WriteLine($"Inline -- Translation -- {"ZAF"} -- Language - cs -- {countryEnglishTranslation.OfficialName}");

// Timezones
var timezone = World.TimeZones.Get("EET")!;

Console.WriteLine($"Inline -- TimeZone -- {timezone.Id} -- {timezone.Type}");

// Machine
// Mac address
var macAddress = Machine.MacAddresses.Get("00-16-F6-11-22-33")!;

Console.WriteLine($"Inline -- MacAddress -- {macAddress.Id} -- {macAddress.OrganizationName}");

// Dependency injection flow

// Setup
var serviceCollection = new ServiceCollection();
serviceCollection.AddWorldContext();
serviceCollection.AddMachineContext();

var serviceProvider = serviceCollection.BuildServiceProvider();

// World context
// Timezones
var worldContextDi = serviceProvider.GetRequiredService<IWorldInfoContext>();
timezone = worldContextDi.TimeZones.Get("EET")!;

Console.WriteLine($"DI -- TimeZone -- {timezone.Id} -- {timezone.Type}");

// Machine context
// Mac address
var machineContextDi = serviceProvider.GetRequiredService<IMachineInfoContext>();
macAddress = machineContextDi.MacAddresses.Get("00-16-F6-11-22-33")!;

Console.WriteLine($"DI -- MacAddress -- {macAddress.Id} -- {macAddress.OrganizationName}");
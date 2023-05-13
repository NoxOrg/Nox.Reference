using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data;
using Nox.Reference.Data.Machine;
using Nox.Reference.Data.World;
using Nox.Reference.Data.World.Extensions.Queries;

Console.WriteLine("This is Nox.Reference Demo!");

// Simplified

var worldContext2 = WorldDataContext.Create();
var timezone = worldContext2.TimeZones.Get("EET")!;

Console.WriteLine($"{timezone.Id} -- {timezone.Type}");

var machineContext2 = MachineDataContext.Create();
var macAddress = machineContext2.MacAddresses.Get("B0D888")!;

Console.WriteLine($"{macAddress.Id} -- {macAddress.OrganizationName}");

// From DI

var serviceCollection = new ServiceCollection();
serviceCollection.AddWorldContext();

var serviceProvider = serviceCollection.BuildServiceProvider();

var worldContext = serviceProvider.GetRequiredService<IWorldInfoContext>();
timezone = worldContext.TimeZones.Get("EET")!;

Console.WriteLine($"{timezone.Id} -- {timezone.Type}");

var machineContext = serviceProvider.GetRequiredService<IMachineInfoContext>();
macAddress = machineContext.MacAddresses.Get("B0D888")!;

Console.WriteLine($"{macAddress.Id} -- {macAddress.OrganizationName}");
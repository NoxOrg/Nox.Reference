using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data;
using Nox.Reference.Data.World;
using Nox.Reference.Data.World.Extensions.Queries;

Console.WriteLine("This is Nox.Reference Demo!");

// Simplified

var context2 = WorldDataContext.Create();
var timezone = context2.TimeZones.Get("EET")!;

Console.WriteLine($"{timezone.Id} -- {timezone.Type}");

// From DI

var serviceCollection = new ServiceCollection();
serviceCollection.AddWorldContext();

var serviceProvider = serviceCollection.BuildServiceProvider();

var context = serviceProvider.GetRequiredService<IWorldInfoContext>();
timezone = context.TimeZones.Get("EET")!;

Console.WriteLine($"{timezone.Id} -- {timezone.Type}");
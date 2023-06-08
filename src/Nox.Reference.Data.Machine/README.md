#Nox.Refence.Machine project

#How to use:
There are 2 approaches how to deal with Nox.Refence.World:
	1. Static approach: 
		Allow to call extension methods or run query over quryable properties.
		Currency currency = Machine.MacAddresses.Get("0016F6");
		or
		Currency currency = Machine.MacAddresses.FirstOrDefault(x => x.Prefix == "0016F6");
	
	
	2. Use dependency injection:
		- Initially World data context should be registered in dependency container
		services.AddWorldContext();
		
		Then is simply be aquired using DI
		class ConsumerService
		{
			private readonly IMachineInfoContext _machineContext;
			
			public ConsumerService(IMachineInfoContext wordContext)
			{
				_machineContext = machineContext;
			}
			
			public void Test()
			{
				MacAddress macAddress = _machineContext.MacAddressess.FirstOrDefault(x => x.Prefix == "0016F6");
				//or
				MacAddress macAddress = _machineContext.MacAddressess.Get("0016F6");
			}
		}
		
#How to create migrations:
- In powershel or similar command tool go to Nox.Reference\src\Nox.Reference.Data.World
- Run the following command:   dotnet ef  migrations add  <MigrationName>  --project ../NoNox.Reference.Data.World/Nox.Reference.Data.World.csproj
- Created migration will appear in Nox.Reference.Data.World\\Mirations folder

To create or update database:
Run command dotnet ef database update --connection "Data Source=..\\..\\data\\noxreferences.db"

noxreferences.db database file will appear in (RootPath)//data


#Project structure		
Nox.Refence.Machine contains the following entities:
	- MacAddress
# Nox.Reference.Machine project
*Nox.Reference.Machine* project contains functionality and classes to get common information about MacAddresses.

## How to use:
There are 2 approaches how to deal with Nox.Refence.World:
### 1. Static approach: 
Allow to call extension methods or run a query over queryable properties.

`Currency currency = Machine.MacAddresses.Get("0016F6");`

or

`Currency currency = Machine.MacAddresses.FirstOrDefault(x => x.Prefix == "0016F6");`

### 2. Dependency injection:
- Initially, Machine data context should be registered in the dependency container

`services.AddMachineContext();`

		
- Then is simply be acquired using DI

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
		
		
### How to create migrations:
- In Powershell or a similar command tool go to Nox.Reference\src\Nox.Reference.Data.Machine
- Run the following command:   
`dotnet ef  migrations add  <MigrationName>  --project ../NoNox.Reference.Data.World/Nox.Reference.Data.Machine.csproj`
- Created migration will appear in Nox.Reference.Data.Machine\\Migrations folder

To create or update the database:
Run command 
`dotnet ef database update --connection "Data Source=..\\..\\data\\Nox.Reference.Machine.db"`

Nox.Reference.Machine.db database file will appear in (RootPath)\data\output\sqlite


## Project structure		
Nox.Reference.Machine contains the following entities:
- MacAddress
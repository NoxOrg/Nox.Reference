#Nox.Refence.World project

#How to use:
There are 2 approaches how to deal with Nox.Refence.World:
	1. Static approach: 
		Allow to call extension methods or run query over quryable properties.
		Currency currency = World.Currencies.Get("USD");
		or
		Currency currency = World.Currencies.FirstOrDefault(x => x.IsoCode == isoCode);
	
	
	2. Use dependency injection:
		- Initially World data context should be registered in dependency container
		services.AddWorldContext();
		
		Then is simply be aquired using DI
		class ConsumerService
		{
			private readonly IWorldInfoContext _wordContext;
			
			public ConsumerService(IWorldInfoContext wordContext)
			{
				_wordContext = wordContext;
			}
			
			public void Test()
			{
				Currency currency = _wordContext.Currencies.FirstOrDefault(x => x.IsoCode == isoCode);
				//or
				Currency currency = _wordContext.Currencies.Get(isoCode);
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
Nox.Refence.World contains the following entities:
	- AlternateSpelling
	- CarrierPhoneNumber
	- CoatOfArms
	- Continent
	- Country
	- CountryCapital
	- CountryDialing
	- CountryFlag
	- CountryHoliday
	- CountryMaps
	- CountryNames
	- CountryNameTranslation
	- CountryNativeName
	- CountryVehicle
	- Culture
	- Currency
	- CurrencyFrequentUsage
	- CurrencyRareUsage
	- CurrencyUsage
	- DateFormat
	- Demonymn
	- GeoCoordinates
	- GiniCoefficient
	- HolidayData
	- Language
	- LanguageTranslation
	- LocalHolidayName
	- MajorCurrencyUnit
	- MinorCurrencyUnit
	- NumberFormat
	- PhoneCarrier
	- PostalCode
	- RegionHoliday
	- StateHoliday
	- TimeZone
	- TopLevelDomain
	- VatNumberDefinition
	- VatNumberValidationRule
	

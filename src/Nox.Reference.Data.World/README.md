# Nox.Refence.World project
*Nox.Refence.World* project contains functionality and classes to get common information about Countries, Currencies, Languages, CountryHolidays etc.
## How to use:
There are 2 approaches how to deal with Nox.Refence.World:
### 1. Static approach: 
Allow to call extension methods or run query over quryable properties.

`
Currency currency = World.Currencies.Get("USD");
`

or
		
`
Currency currency = World.Currencies.FirstOrDefault(x => x.IsoCode == isoCode);
		`
	
### 2. Use dependency injection:
- Initially World data context should be registered in dependency container
`
services.AddWorldContext();
`


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

## How to create migrations:
- In powershel or similar command tool go to Nox.Reference\src\Nox.Reference.Data.World
- Run the following command:   dotnet ef  migrations add  <MigrationName>  --project ../NoNox.Reference.Data.World/Nox.Reference.Data.World.csproj
- Created migration will appear in Nox.Reference.Data.World\\Migrations folder

To create or update database:
Run command dotnet ef database update --connection "Data Source=..\\..\\data\\Nox.Reference.Data.World.db"

Nox.Reference.World.db database file will appear in (RootPath)\data\output\sqlite
	
## Project structure		
Nox.Reference.World contains the following entities:

	- Country
		- AlternateSpelling
		- CoatOfArms
		- Continent
		- CountryCapital
		- CountryDialing
		- CountryFlag
		- CountryHoliday
		- CountryMaps
		- CountryNames
		- CountryNameTranslation
		- CountryNativeName
		- CountryVehicle
		- Demonymn
		- GeoCoordinates
		- GiniCoefficient
		- TopLevelDomain
		- PostalCode
	- Culture
		- DateFormat
			- TimeZone
		- NumberFormat

	- Currency
		- CurrencyUsage
			- CurrencyFrequentUsage
			- CurrencyRareUsage
		- MajorCurrencyUnit
		- MinorCurrencyUnit

	- HolidayData
		- RegionHoliday
		- StateHoliday
		- LocalHolidayName
		
	- Language
		- LanguageTranslation

	- VatNumberDefinition
		- VatNumberValidationRule
		
	- PhoneCarrier
		- CarrierPhoneNumber
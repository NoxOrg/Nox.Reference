# Nox.Reference.World project
*Nox.Reference.World* project contains functionality and classes to get common information about Countries, Currencies, Languages, Holidays, etc.
## How to use:
There are 2 approaches how to deal with Nox.Reference.World:
### 1. Static approach: 
Allow to call extension methods or run a query over queryable properties.

`
Currency currency = World.Currencies.Get("USD");
`

or
		
`
Currency currency = World.Currencies.FirstOrDefault(x => x.IsoCode == isoCode);
		`
	
### 2. Use dependency injection:
- Initially, World data context should be registered in the dependency container

```

services.AddWorldContext();

```

- Then is simply be acquired using DI

```

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
```

More examples you can find in  [Nox.Reference.Demo project](https://github.com/NoxOrg/Nox.Reference/blob/main/src/Nox.Demo/Program.cs "Examples")

## How to create migrations:
- In powershel or a similar command tool go to Nox.Reference\src\Nox.Reference.Data.World
- Run the following command:   

`dotnet ef  migrations add  <MigrationName>  --project ../Nox.Reference.Data.World/Nox.Reference.Data.World.csproj`

- Created migration will appear in Nox.Reference.Data.World\\Migrations folder

To create or update the database:

Run command

`
dotnet ef database update --connection "Data Source=..\\..\\data\\Nox.Reference.Data.World.db"
`

Nox.Reference.World.db database file will appear in (RootPath)\data\output\sqlite


## How to override VatValidation
In case a user is supposed to override VatNumber validation with custom rules it's possible just by implementing an interface

```

public interface IVatValidationService
{
    VatNumberValidationResult ValidateVatNumber(
        string vatNumber,
        bool shouldValidateViaApi = true);

    string CountryCode { get; }
}

```

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
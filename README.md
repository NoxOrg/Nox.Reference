# About 

***Nox.Reference*** is a reference library that provides convenient access to universal information like country, machine, and IP address.
For data persistence Nox.Reference uses [SQLite](https://www.sqlite.org/index.html) databases which are divided by domain-specific responsibilities. 

Nox.Reference represents the following packages:

- Nox.Reference.World
- Nox.Reference.Machine
- Nox.Reference.IpAddress


## Solution design convention:
- All entities in a project should be placed in the folder {ProjectName}/Entities/{Plural Entity Name}/{Singular Entity Name}.cs
- All entity configuration in a project should be placed in the folder {ProjectName}/Configurations/{Plural Entity Name}Configuration.cs
- All data seeder in a project should be placed in the folder {ProjectName}/Seeds/{Plural Entity Name}DataSeeder.cs
- All data mapping in a project should be placed in the folder {ProjectName}/Mappings/{Plural Entity Name}Mapping.cs
- Configuration and DataSeeder classes should have an internal access level to avoid being exposed to external usages.


## Nox.Reference.Data.Common
Contains common logic to facilitate implementation and invocation for major entities and their configurations.
IKeyedNoxReferenceEntity<TKey>  - use this interface when an entity is bound to contain Id field. Id property type can vary.
NoxReferenceEntityBase - base class for all entities intended to be stored in a database.
NoxReferenceDataSeederBase<TDbContext, TSource, TEntity>  - base class for data seeder to load and transform input data to entities.
EnumGeneratorService - a class that can be used for enum generation. These enums help to get static data and are usually used as parameters for methods that obtain data. 



NoxReferenceDataSeederBase<TDbContext, TSource, TEntity> contains the following properties which should be overridden in derived classes:
    public override string TargetFileName => "Nox.Reference.Currencies.json"; // File which presists transformed data in json.
    public override string DataFolderPath => "Currencies"; // Folder name in output folder.


## To create a new entity in any project do the following steps:
- Define the class inherited from NoxReferenceEntityBase.
(in case the entity supports Id property implement  IKeyedNoxReferenceEntity<TKey> interface with the necessary type).

```

public class Country : NoxReferenceEntityBase, IKeyedNoxReferenceEntity<string>
{
	// Should be defined as how to build the key.
	// Id field is not persisted in a database.
	public string Id => Code;
}

```


- Create a configuration for an entity in appropriated folder

```

internal class CultureConfiguration : NoxReferenceEntityConfigurationBase<Culture>
{
}

```

If entity supports Id property then derive from class.

`
NoxReferenceKeyedEntityConfigurationBase<TEntity, TKey>  
`

*(Note: it doesn't need to register configuration in dataContext class, all configurations will be registered automatically)*


## How to write custom data seeder.

DataSeeder is a class that serves to load external data with dto and transform them into matching entities.

- Create a class according to name convention (For example CountryDataSeeder.cs)

- It can implement interface ```INoxReferenceDataSeeder```. Write any custom logic in ```Seed()``` method.

- To significantly reduce common work it is possible to derive from ```NoxReferenceDataSeederBase<,,>``` class that already implements common logic.

```
public class CountryDataSeeder : NoxReferenceDataSeederBase<WorldDbContext, CountryInfo, Country>
```

- Override method ```IReadOnlyList<TSource> GetFlatEntitiesFromDataSources()```

- Register data seeder in the data flow in the following line in DataSeederExtensions file.

```
services.AddScoped<INoxReferenceDataSeeder, CurrencyDataSeeder>(); // you can exclude dataloader from flow if necessary, especially for debug purposes.
```

## How to transform input data into an entity.
-   Create an [Automapper](https://automapper.org/) profile and set up a mapping

```
 internal class CurrencyMapping : Profile
```

- For a complex scenario like resolving related entities or just using dependency injection during the transformation use the approach outlined in the example below:
 
```
CreateMap<string, Country>().ConvertUsing<CountrySingleMapping>();
```

```
internal  CountrySingleMapping : ITypeConverter<string, Country>{}

```

 - To convert an entity to a DTO Nox.Reference provides an extension method of any entity derived from NoxReferenceEntityBase ToDto<>() method should be typed by an appropriate DTO which is already mapped to the entity.
Also, there is an overload ToDto<>() which facilitates handling the conversion list of DTOs to entities.  
 
 
## How to add migration for a particular data context :
 - Open Developer Powershell in Visual Studio
 - Go to the ceratin project folder: 
ls  Nox.Reference\src\Nox.Reference.Data.World
 - Run command 
 
 ```
 dotnet ef  migrations add {MigrationName}  -s Nox.Reference.Data.World.csproj

```
If the command ran successfully migration will be created.
 - Then apply migration over the database
 
```
  dotnet ef database update --connection "Data Source=..\\..\\data\\output\\sqlite\\NoxReference.World.db"

```
## How to bump package version locally:
- from the root folder run script bump-version 
```
goo bump-version {versionNumber}
```
- run script move-nugets
```
goo move-nugets
```
- copy packages from generated-packages folder  to LocalFeed  folder 
*if LocalFeed folder does not exist then add [LocalFeed](https://learn.microsoft.com/en-us/nuget/hosting-packages/local-feeds "How to create LocalFeed") folder to solution*


## How to change configuration settings:

There is a file noxReferenceSettings.json in Nox.Reference.Common project.

```
{
  "ConnectionStrings": {
    "NoxReferenceMachineConnection": "Data Source=.\\NoxReferenceDatabase\\Nox.Reference.Machine.db",
    "NoxReferenceWorldConnection": "Data Source=.\\NoxReferenceDatabase\\Nox.Reference.World.db",
    "NoxReferenceDataLoadWorldConnection": "Data Source=..\\..\\..\\..\\..\\data\\output\\sqlite\\Nox.Reference.World.db", 
    "NoxReferenceDataLoadMachineConnection": "Data Source=..\\..\\..\\..\\..\\data\\output\\sqlite\\Nox.Reference.Machine.db"
  },
  "SourceDataPath": "..\\..\\..\\..\\..\\data\\input\\source\\",
  "TargetDataPath": "..\\..\\..\\..\\..\\data\\output\\raw-json\\",
  "UriMacAddresses": "https://standards-oui.ieee.org/oui/oui.csv",
  "UriRestWorldCurrencies": "https://raw.githubusercontent.com/wiredmax/world-currencies/master/dist/json/currencies.json",
  "UriRestCurrencyFormatterCurrencies": "https://raw.githubusercontent.com/smirzaei/currency-formatter/master/currencies.json",
  "UriRestCountries": "https://gitlab.com/restcountries/restcountries/-/raw/master/src/main/resources/countriesV3.1.json",
  "UriLanguagesISO639": "https://raw.githubusercontent.com/scsmith/language_list/master/data/languages.yml",
  "UriLanguagesAdditionalInfo": "https://raw.githubusercontent.com/haliaeetus/iso-639/master/data/iso_639-2.json",
  "VatNumberDefinitionDataPath": "..\\..\\..\\..\\..\\data\\Nox.Reference.VatNumbers.json",
  "HolidaysZipPath": "..\\..\\..\\..\\..\\data\\",
  "UriLocalePlanetList": "https://www.localeplanet.com/icu/index.html",
  "UriLocalePlanetItem": "https://www.localeplanet.com/icu/{localeCode}/index.html",
  "TimeZoneUrl": "https://en.wikipedia.org/wiki/List_of_tz_database_time_zones",
  "NodaTimeUrl": "https://nodatime.org/timezones#notes",
  "PhoneCarrierDataPath": "..\\..\\..\\..\\..\\data\\input\\source\\PhoneNumbers\\Nox.Reference.PhoneNumberCarriers.json"
}

```

To change the databases output folder replace the following line:
```
"NoxReferenceDataLoadWorldConnection": "Data Source={path}"
"NoxReferenceDataLoadMachineConnection": "Data Source={path}"
```

*SourceDataPath* - a folder where source data from external resources are saved.
*TargetDataPath* - output folder for JSON files which are generated during gathering data from external sources.
# About 

***Nox.Refence*** is a storage which contains the most common used types.
To persist data Nox.Refence uses sqlLite databases which are dived by domain specific responsabilites. 


Nox.Refence solution consists of the following projects:

- Nox.Refence.World
- Nox.Refence.Machine


## Soluition design convention:
- All entities in a project should be placed in the folder {ProjectName}/Entities/{Plural Entity Name}/{Singular Entity Name}.cs
- All entity configuration in a project should be placed in the folder {ProjectName}/Configurations/{Plural Entity Name}Configuration.cs
- All data seeder in a project should be placed in the folder {ProjectName}/Seeds/{Plural Entity Name}DataSeeder.cs
- All data mapping in a project should be placed in the folder {ProjectName}/Mappings/{Plural Entity Name}Mapping.cs
- Configuration, DataSeeder classes should have internal access level in order not to be exposed for external usages.


## Nox.Reference.Data.Common
Contains common logic to faciliate implementation and invocation for major entities and their configurations.
IKeyedNoxReferenceEntity<TKey>  - use this interface when entity is bind to contain Id field. Id property type can vary.
NoxReferenceEntityBase - base class for all entities intended to store in database.
NoxReferenceDataSeederBase<TDbContext, TSource, TEntity>  - base class for data seeder to load and transform input data to entities.
EnumGeneratorService - class which serves for enum generation. These enums help to get static data and ussualy used as parameters for methods which obtain data. 



NoxReferenceDataSeederBase<TDbContext, TSource, TEntity>  contain the following properties which should be overriden in derived classes:
    public override string TargetFileName => "Nox.Reference.Currencies.json"; // File which presists transformed data in json.
    public override string DataFolderPath => "Currencies"; // Folder name in output folder.


#In order to create new entity in any project do the following step:
- Define the class inherited from NoxReferenceEntityBase.
(in case entity supports Id property implement  IKeyedNoxReferenceEntity<TKey> interface with necessary type).

```

public class Country : NoxReferenceEntityBase, IKeyedNoxReferenceEntity<string>
{
	// Should be defined how to build the key.
	// Id field is not persisted in a database.
    public string Id => Code;
}

```


- Create configuration for entity in appropriated folder

```

internal class CultureConfiguration : NoxReferenceEntityConfigurationBase<Culture>
{
}

```

If entity supports Id property then derive from class.

`
NoxReferenceKeyedEntityConfigurationBase<TEntity, TKey>  
`

*(Note: it shouldn't register configuration in dataContext, it will be registered automatiacally)*


## How to write custom data seeder.
DataSeeder is a class which serves to load and transform data for certain entity.

-  Create class according to name convention (For example: CountryDataSeeder.cs) and derive it from NoxReferenceDataSeederBase<WorldDbContext, CountryInfo, Country>
public class CountryDataSeeder : NoxReferenceDataSeederBase<WorldDbContext, CountryInfo, Country>
- Next step is register dataSeeder in transforming data flow:
Add the following line in DataSeederExtensions file.
services.AddScoped<INoxReferenceDataSeeder, CurrencyDataSeeder>(); // you can exlude dataloader from flow if necessary, especially in debug purposes.

## How to transform input data to entity.
-   Create automapper profile and setup a mapping
 internal class CurrencyMapping : Profile
- For complex scenario like resolving related entity or just use DI during the transformation use the following approach:
 For example: CreateMap<string, Country>().ConvertUsing<CountrySingleMapping>();
 internal  CountrySingleMapping : ITypeConverter<string, Country>{}
 - To convert entity to dto Nox.Reference provides extension method of any entity derived from NoxReferenceEntityBase ToDto<>() method should be typed by appropriated dto which already mapped with entity.
Also, there is overload  ToDto<>( to faciliteate handling convertion list of dto to entities.  
 
 
## How to add migration for particular data context :
 - Open Developer Powershel in Visual Studion
 - Go to ceratin project folder: 
ls  Nox.Reference\src\Nox.Reference.Data.World
 - Run command 
 
 ```
 dotnet ef  migrations add {MigrationName}  -s Nox.Reference.Data.World.csproj

```
If command ran successful migration will be created.
 - Then apply migration over database
 
```
  dotnet ef database update --connection "Data Source=..\\..\\data\\output\\sqlite\\NoxReference.World.db"

```

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

To change databases output folder replace the following line:
```
"NoxReferenceDataLoadWorldConnection": "Data Source={path}"
"NoxReferenceDataLoadMachineConnection": "Data Source={path}"
```

*SourceDataPath* - a folder where source data from external resources are saved.
*TargetDataPath* - output folder for json files which are generated during gathering data from external sources.
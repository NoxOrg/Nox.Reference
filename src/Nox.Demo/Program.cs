//using Microsoft.Extensions.DependencyInjection;
//using Nox.Reference.Data;
//using Nox.Reference.Data.Machine;
//using Nox.Reference.Data.World;
//using Nox.Reference.Data.World.Extensions.Queries;
//using Nox.Reference.Data.World.Models;
//using System.Text.Json;

//Console.WriteLine("This is Nox.Reference Demo!");

//// TODO: redirect to nuget feed packages instead of local

//// Simplified flow

//// World
//// Country
//var ukraine1 = World.Countries.Get("UKR");
//var ukraine2 = World.Countries.First(x => x.FipsCode == "UP");
//var ukraine3 = World.Countries.GetByAlpha2Code("UA");

//Console.WriteLine($"Inline -- Country -- {ukraine1!.AlphaCode3} -- {ukraine1.Names.CommonName}");
//Console.WriteLine($"Inline -- Country -- {ukraine2.FipsCode} -- {ukraine1.Names.CommonName}");
//Console.WriteLine($"Inline -- Country -- {ukraine3!.AlphaCode2} -- {ukraine1.Names.CommonName}");

//var countryEnglishTranslation = World.Countries.Get("ZAF")!.NameTranslations.FirstOrDefault(x => x.Language.Iso_639_1 == "cs")!;
//Console.WriteLine($"Inline -- Translation -- {"ZAF"} -- Language - cs -- {countryEnglishTranslation.OfficialName}");

//// Cultures
//var culture = World.Cultures.Get("tg-TJ")!;

//Console.WriteLine($"Inline -- Culture -- {culture.Name} -- {culture.DisplayName}");

//// Currencies
//var currency = World.Currencies.Get("TWD")!;

//Console.WriteLine($"Inline -- Currency -- {currency.IsoCode} -- {currency.Name}");

//// Holidays
//var holidays = World.Holidays.Get(2024, "AD")!;

//Console.WriteLine($"Inline -- Holidays -- {holidays.CountryName} - {holidays.Year} -- {holidays.Holidays.Count}");

//// Languages
//var language = World.Languages.GetByIso_639_2t("ces")!;

//Console.WriteLine($"Inline -- Language -- {language.Iso_639_3} -- {language.Name}");

//// Timezones
//var timezone = World.TimeZones.Get("EET")!;

//Console.WriteLine($"Inline -- TimeZone -- {timezone.Code} -- {timezone.Type}");

//// VatNumberDefinitions
//var validationSuccessResult = World.VatNumberDefinitions.Validate("ES", "B65296485", true)!;
//var validationFailResult = World.VatNumberDefinitions.Validate("ES", "BROKEN", true)!;

//Console.WriteLine($"Inline -- VatNumberDefinitions -- {validationSuccessResult.Country} -- {validationSuccessResult.FormattedVatNumber} -- {validationSuccessResult.Status}");
//Console.WriteLine($"Inline -- VatNumberDefinitions -- {validationFailResult.Country} -- {validationFailResult.FormattedVatNumber} -- {validationFailResult.Status}");

//// Phone
//var phone = World.PhoneNumbers.GetPhoneNumberInfo("+380965370000", "UA");

//Console.WriteLine($"Inline -- PhoneNumbers -- {phone.FormattedNumber} -- {phone.CarrierName}");

//// Machine
//// Mac address
//var macAddress = Machine.MacAddresses.Get("00-16-F6-11-22-33")!;

//Console.WriteLine($"Inline -- MacAddress -- {macAddress.MacPrefix} -- {macAddress.OrganizationName}");

//// Dependency injection flow

//// Setup
//var serviceCollection = new ServiceCollection();
//serviceCollection.AddWorldContext();
//serviceCollection.AddMachineContext();

//var serviceProvider = serviceCollection.BuildServiceProvider();

//// World context
//var worldContextDi = serviceProvider.GetRequiredService<IWorldInfoContext>();

//// Country
//ukraine1 = worldContextDi.Countries.Get("UKR");
//ukraine2 = worldContextDi.Countries.First(x => x.FipsCode == "UP");
//ukraine3 = worldContextDi.Countries.GetByAlpha2Code("UA");

//Console.WriteLine($"Inline -- Country -- {ukraine1!.Name} -- {ukraine1.Names.CommonName}");
//Console.WriteLine($"Inline -- Country -- {ukraine2.FipsCode} -- {ukraine1.Names.CommonName}");
//Console.WriteLine($"Inline -- Country -- {ukraine3!.AlphaCode2} -- {ukraine1.Names.CommonName}");

//countryEnglishTranslation = worldContextDi.Countries.Get("ZAF")!.NameTranslations.FirstOrDefault(x => x.Language.Iso_639_1 == "cs")!;
//Console.WriteLine($"Inline -- Translation -- {"ZAF"} -- Language - cs -- {countryEnglishTranslation.OfficialName}");

//// Timezones
//timezone = worldContextDi.TimeZones.Get("EET")!;

//Console.WriteLine($"DI -- TimeZone -- {timezone.Code} -- {timezone.Type}");

//// Machine context
//var machineContextDi = serviceProvider.GetRequiredService<IMachineInfoContext>();

//// Mac address
//macAddress = machineContextDi.MacAddresses.Get("00-16-F6-11-22-33")!;

//Console.WriteLine($"DI -- MacAddress -- {macAddress.MacPrefix} -- {macAddress.OrganizationName}");

//// Automapper example
//var ukraineMapped = World.Mapper.Map<CountryInfo>(ukraine1);
//Console.WriteLine("Serialized data:");
//Console.WriteLine(JsonSerializer.Serialize(ukraineMapped, new System.Text.Json.JsonSerializerOptions
//{
//    WriteIndented = true,
//}));
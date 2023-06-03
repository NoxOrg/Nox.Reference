using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data;
using Nox.Reference.Data.Machine;
using Nox.Reference.Data.World;
using Nox.Reference.Data.World.Extensions.Queries;
using Nox.Reference.Data.World.Models;
using System.Text.Json;

Console.WriteLine("This is Nox.Reference Demo!");

// Simplified flow

// World
// Country
Country ukraine1 = World.Countries.Get("UA")!;
var ukraine2 = World.Countries.First(x => x.FipsCode == "UP");
var ukraine3 = World.Countries.GetByAlpha3Code("UKR");

Console.WriteLine($"Inline -- Country -- {ukraine1!.AlphaCode3} -- {ukraine1.Names.CommonName}");
Console.WriteLine($"Inline -- Country -- {ukraine2.FipsCode} -- {ukraine1.Names.CommonName}");
Console.WriteLine($"Inline -- Country -- {ukraine3!.AlphaCode2} -- {ukraine1.Names.CommonName}");

CountryNameTranslation countryEnglishTranslation = World.Countries.Get("ZAF")!.NameTranslations.FirstOrDefault(x => x.Language.Iso_639_1 == "cs")!;
Console.WriteLine($"Inline -- Translation -- {"ZAF"} -- Language - cs -- {countryEnglishTranslation.OfficialName}");

// Cultures
Culture culture = World.Cultures.Get("tg-TJ")!;

Console.WriteLine($"Inline -- Culture -- {culture.Name} -- {culture.DisplayName}");

// Currencies
Currency currency = World.Currencies.Get("TWD")!;

Console.WriteLine($"Inline -- Currency -- {currency.IsoCode} -- {currency.Name}");

// Holidays
CountryHoliday holidays = World.Holidays.Get(2024, "AD")!;

Console.WriteLine($"Inline -- Holidays -- {holidays.CountryName} - {holidays.Year} -- {holidays.Holidays.Count}");

// Languages
Language language = World.Languages.GetByIso_639_2t("ces")!;

Console.WriteLine($"Inline -- Language -- {language.Iso_639_3} -- {language.Name}");

// Timezones
Nox.Reference.Data.World.TimeZone timezone = World.TimeZones.Get("EET")!;

Console.WriteLine($"Inline -- TimeZone -- {timezone.Code} -- {timezone.Type}");

// VatNumberDefinitions
VatNumberValidationResult validationSuccessResult = World.VatNumberDefinitions.Validate("ES", "B65296485", true)!;
var validationFailResult = World.VatNumberDefinitions.Validate("ES", "BROKEN", true)!;

Console.WriteLine($"Inline -- VatNumberDefinitions -- {validationSuccessResult.Country} -- {validationSuccessResult.FormattedVatNumber} -- {validationSuccessResult.Status}");
Console.WriteLine($"Inline -- VatNumberDefinitions -- {validationFailResult.Country} -- {validationFailResult.FormattedVatNumber} -- {validationFailResult.Status}");

// Phone
PhoneNumberInfo phone = World.PhoneNumbers.GetPhoneNumberInfo("+380965370000", "UA");

Console.WriteLine($"Inline -- PhoneNumbers -- {phone.FormattedNumber} -- {phone.CarrierName}");

// Machine
// Mac address
MacAddress macAddressDash = Machine.MacAddresses.Get("00-16-F6-11-22-33")!;
MacAddress macAddressSemi = Machine.MacAddresses.Get("00:16:F6:11:22:33")!;
MacAddress macAddressNoSpace = Machine.MacAddresses.Get("0016F6112233")!;
MacAddress macAddressSpace = Machine.MacAddresses.Get("00 16 F6 11 22 33")!;

Console.WriteLine($"Inline -- MacAddress -- {macAddressDash.MacPrefix} -- {macAddressDash.OrganizationName}");
Console.WriteLine($"Inline -- MacAddress -- {macAddressSemi.MacPrefix} -- {macAddressSemi.OrganizationName}");
Console.WriteLine($"Inline -- MacAddress -- {macAddressNoSpace.MacPrefix} -- {macAddressNoSpace.OrganizationName}");
Console.WriteLine($"Inline -- MacAddress -- {macAddressSpace.MacPrefix} -- {macAddressSpace.OrganizationName}");

// Dependency injection flow

// Setup
var serviceCollection = new ServiceCollection();
serviceCollection.AddWorldContext();
serviceCollection.AddMachineContext();

var serviceProvider = serviceCollection.BuildServiceProvider();

// World context
IWorldInfoContext worldContextDi = serviceProvider.GetRequiredService<IWorldInfoContext>();

// Country
ukraine1 = worldContextDi.Countries.Get("UKR")!;
ukraine2 = worldContextDi.Countries.First(x => x.FipsCode == "UP");
ukraine3 = worldContextDi.Countries.GetByAlpha2Code("UA");

Console.WriteLine($"Inline -- Country -- {ukraine1!.Name} -- {ukraine1.Names.CommonName}");
Console.WriteLine($"Inline -- Country -- {ukraine2.FipsCode} -- {ukraine1.Names.CommonName}");
Console.WriteLine($"Inline -- Country -- {ukraine3!.AlphaCode2} -- {ukraine1.Names.CommonName}");

countryEnglishTranslation = worldContextDi.Countries.Get("ZAF")!.NameTranslations.FirstOrDefault(x => x.Language.Iso_639_1 == "cs")!;
Console.WriteLine($"Inline -- Translation -- {"ZAF"} -- Language - cs -- {countryEnglishTranslation.OfficialName}");

// Timezones
timezone = worldContextDi.TimeZones.Get("EET")!;

Console.WriteLine($"DI -- TimeZone -- {timezone.Code} -- {timezone.Type}");

// Machine context
var machineContextDi = serviceProvider.GetRequiredService<IMachineInfoContext>();

// Mac address
macAddressDash = machineContextDi.MacAddresses.Get("00-16-F6-11-22-33")!;

Console.WriteLine($"DI -- MacAddress -- {macAddressDash.MacPrefix} -- {macAddressDash.OrganizationName}");

// Automapper example
CountryInfo ukraineMapped = World.Mapper.Map<CountryInfo>(ukraine1);
Console.WriteLine("Serialized data:");
Console.WriteLine(JsonSerializer.Serialize(ukraineMapped, new JsonSerializerOptions
{
    WriteIndented = true,
}));

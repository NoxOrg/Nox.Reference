# Nox.Reference.Countries
Countries list and info

## Usage example

```csharp
using System.Text.Json;
using Nox.Reference.Countries;

ICountriesService countryService = new CountriesService();

ICountryInfo info = countryService.GetCountries(); 

var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

Console.WriteLine(JsonSerializer.Serialize(info,options));
```

<details>
  <summary>Output example</summary>

```csharp
/* Outputs:
  {
    {
      "id": 710,
      "name": "South Africa",
      "code": "ZAF",
      "languages": [
        "afr",
        "eng",
        ...
      ],
      "names": {
        "commonName": "South Africa",
        "officialName": "Republic of South Africa",
        "nativeNames": null
      },
      "topLevelDomains": [
        ".za"
      ],
      "alphaCode2": "ZA",
      "numericCode": "710",
      "alphaCode3": "ZAF",
      "olympicCommitteeCode": "RSA",
      "fifaCode": "RSA",
      "fipsCode": "SF",
      "isIndependent": true,
      "codeAssignedStatus": "officially-assigned",
      "isUnitedNationsMember": true,
      "currencies": [
        "ZAR"
      ],
      "dialingInfo": {
        "prefix": "\u002B2",
        "suffixes": [
          "7"
        ]
      },
      "capitals": [
        "Pretoria",
        "Bloemfontein",
        "Cape Town"
      ],
      "capitalInfo": {
        "latLong": [
          -25.7,
          28.22
        ]
      },
      "alternateSpellings": [
        "ZA",
        "RSA",
        "Suid-Afrika",
        "Republic of South Africa"
      ],
      "region": "Africa",
      "subRegion": "Southern Africa",
      "continents": [
        "Africa"
      ],
      "nameTranslations": [
        {
          "language": "bre",
          "officialName": "Republik Suafrika",
          "commonName": "Suafrika"
        },
        {
          "language": "ces",
          "officialName": "Jihoafrick\u00E1 republika",
          "commonName": "Jihoafrick\u00E1 republika"
        },
        ...
      ],
      "latLong": [
        -29,
        24
      ],
      "isLandlocked": false,
      "borderingCountries": [
        "BWA",
        "LSO",
        ...
      ],
      "landAreaInSquareKilometers": 1221037,
      "emojiFlag": "\uD83C\uDDFF\uD83C\uDDE6",
      "demonyms": [
        {
          "language": "eng",
          "feminine": "South African",
          "masculine": "South African"
        },
        {
          "language": "fra",
          "feminine": "Sud-africaine",
          "masculine": "Sud-africain"
        }
      ],
      "flags": {
        "svg": "https://flagcdn.com/za.svg",
        "png": "https://flagcdn.com/w320/za.png",
        "alternateText": "The flag of South Africa is composed of two equal horizontal bands of red and blue, with a yellow-edged black isosceles triangle superimposed on the hoist side of the field. This triangle has its base centered on the hoist end, spans about two-fifth the width and two-third the height of the field, and is enclosed on its sides by the arms of a white-edged green horizontally oriented Y-shaped band which extends along the boundary of the red and blue bands to the fly end of the field."
      },
      "coatOfArms": {
        "svg": "https://mainfacts.com/media/images/coats_of_arms/za.svg",
        "png": "https://mainfacts.com/media/images/coats_of_arms/za.png"
      },
      "population": 59308690,
      "maps": {
        "googleMaps": "https://goo.gl/maps/CLCZ1R8Uz1KpYhRv6",
        "openStreetMaps": "https://www.openstreetmap.org/relation/87565"
      },
      "giniCoefficients": {
        "2014": 63
      },
      "vehicleInfo": {
        "drivingSide": "left",
        "internationalRegistrationCodes": [
          "ZA"
        ]
      },
      "postalCodeInfo": {
        "format": "####",
        "regex": "^(\\d{4})$"
      },
      "startOfWeek": "monday",
      "startDayOfWeek": 1,
      "timeZones": [
        "UTC\u002B02:00"
      ]
    }
  }
*/
```
</details>

### To install from nuget.org
```powershell
dotnet add package Nox.Reference.Countries
```

### Dependencies
Uses [RestCountries](https://gitlab.com/restcountries/restcountries) library as a data source for countries info.

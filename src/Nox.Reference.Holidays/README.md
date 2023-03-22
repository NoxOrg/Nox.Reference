# Nox.Reference.Holidays
Holidays list and info

## Usage example

```csharp
using System.Text.Json;
using Nox.Reference.Holidays;

var holidaysService = new HolidaysService(2024);

var info = holidaysService.GetHolidays(); 

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
[
    "holidaysByCountries": [
        {
            "country": "US",
            "countryName": "United States of America",
            "dayOff": "Sunday",
            "holidays": [
                {
                    "name": "New Year's Day",
                    "type": "public",
                    "date": "2024-01-01",
                    "localNames": []
                },
                ...
            ],
            "states": [
                {
                    "state": "CA",
                    "stateName": "California",
                    "holidays": [
                        {
                            "name": "New Year's Day",
                            "type": "public",
                            "date": "2024-01-01",
                            "localNames": []
                        },
                        ...
                    ],
                    "regions": [
                        {
                            "region": "AL",
                            "regionName": "Alabama",
                            "holidays": [
                                {
                                    "name": "New Year's Day",
                                    "type": "public",
                                    "date": "2024-01-01",
                                    "localNames": []
                                },
                                ...
                            ]
                        },
                    ]
                }
            ]
        }
    ],
    year:2024
]
*/
```
</details>

### To install from nuget.org
```powershell
dotnet add package Nox.Reference.Holidays
```

### Dependencies
Uses [date-holidays](https://github.com/commenthol/date-holidays) library as a data source for holidays info.

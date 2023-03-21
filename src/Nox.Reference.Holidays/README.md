# Nox.Reference.Holidays
Holidays list and info

## Usage example

```csharp
using System.Text.Json;
using Nox.Reference.Currencies;

var currencyService = new CurrenciesService();

var info = currencyService.GetCurrencies(); 

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
    {
        "isoCode": "UAH",
        "isoNumber": "980",
        "symbol": "₴",
        "thousandsSeparator": " ",
        "decimalSeparator": ",",
        "symbolOnLeft": false,
        "spaceBetweenAmountAndSymbol": false,
        "decimalDigits": 2,
        "name": "Ukrainian Hryvnia",
        "units": {
            "major": {
                "name": "Hryvnia",
                "symbol": "₴"
            },
            "minor": {
                "name": "Kopiyka",
                "symbol": "",
                "majorValue": 0.01
            }
        },
        "banknotes": {
            "frequent": [
                "₴1",
                "₴2",
                "₴5",
                "₴10",
                "₴20",
                "₴50",
                "₴100",
                "₴200",
                "₴500"
            ],
            "rare": [
            ]
        },
        "coins": {
            "frequent": [
                "1",
                "2",
                "5",
                "10",
                "25",
                "50"
            ],
            "rare": [
            ]
        }
    }
]
*/
```
</details>

### To install from nuget.org
```powershell
dotnet add package Nox.Reference.Currencies
```

### Dependencies
Uses [world-currencies](https://github.com/wiredmax/world-currencies) and [currency-formatter](https://github.com/smirzaei/currency-formatter) libraries as a data source for currencies info.

# Nox.Reference.PhoneNumbers
Validate and format VAT numbers

## Usage example

```csharp
using System.Text.Json;
using Nox.Reference.VatNumber.Services;

var service = new VatValidationService();
var validationResult = service.ValidateVatNumber(new VatNumber.Models.VatNumber("123Test456", "UA"));

var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

Console.WriteLine(validationResult);

/* Outputs:
{
      "validationStatus": "Invalid",
      "validationErrors": [
        "Validation of given value '123Test456' has failed. Please, use the following format: 'VAT should have from 8 to 10 numeric characters'."
      ]
    }
*/
```

### To install from nuget.org
```powershell
dotnet add package Nox.Reference.VatNumbers
```

### Dependencies
Uses [CountryValidator](https://github.com/anghelvalentin/CountryValidator) as a partial source of VAT number validation rules.
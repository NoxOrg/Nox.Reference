# Nox.Reference.PhoneNumbers
Validate and format phone numbers

## Usage example

```csharp
using System.Text.Json;
using Nox.Reference.PhoneNumbers;

IPhoneNumberService phoneNumberService = new PhoneNumberService();

IPhoneNumberInfo info = phoneNumberService.GetPhoneNumberInfo("833770694","ZA"); 

var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

Console.WriteLine(JsonSerializer.Serialize(info,options));

/* Outputs:
{
  "inputNumber": "833770694",
  "formattedNumber": "\u002B27833770694",
  "formattedNumberNational": "083 377 0694",
  "formattedNumberInternational": "\u002B27 83 377 0694",
  "formattedNumberRfc3966": "tel:\u002B27-83-377-0694",
  "isValid": true,
  "isMobile": true,
  "numberType": "MOBILE",
  "regionCode": "ZA",
  "regionName": "South Africa",
  "carrierName": "MTN"
}
*/
```

### To install from nuget.org
```powershell
dotnet add package Nox.Reference.PhoneNumbers
```

### Dependencies
Uses Thomas Clegg's [libphonenumber-csharp](https://github.com/twcclegg/libphonenumber-csharp) which is a C# port of [Google's libphonenumber](https://github.com/google/libphonenumber) Java project.

# Nox.Reference.MacAddresses
MacAddresses list and info

## Usage example

```csharp
using System.Text.Json;
using Nox.Reference.MacAddresses;

var macAddressesService = new MacAddressesService();

var info = macAddressesService.GetMacAddresses(); 

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
    "address": "000000",
    "vendor": "Officially Xerox"
  },
  {
    "address": "000001",
    "vendor": "SuperLAN-2U"
  },
  {
    "address": "000002",
    "vendor": "BBN (was internal usage only, no longer used)"
  }...
]
*/
```
</details>

### To install from nuget.org
```powershell
dotnet add package Nox.Reference.MacAddresses
```

### Dependencies

# Nox.Reference.MacAddresses
MacAddresses list and info

## Usage example

```csharp

using System.Text.Json;
using Nox.Reference.MacAddresses;

public class TestClass
{
    private readonly IMacAddressesService _macAddressesService;

    public TestClass(IMacAddressesService macAddressesService)
    {
        _macAddressesService = macAddressesService;
    }

    public void Test()
    {
        var info = _macAddressesService.GetMacAddresses(); 

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        Console.WriteLine(JsonSerializer.Serialize(info,options));
    }
}


public class Startup
{
    ConfigureServices(IServiceCollection services)
    {
        services.AddMacAddresses();
    }
}
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

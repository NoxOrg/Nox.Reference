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
        var info = _macAddressesService.GetMacAddressInfo("FC:59:C0:FF:EF:57"); 

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
        services.AddNoxMacAddresses();
    }
}
```

<details>
  <summary>Output example</summary>

```csharp
/* Outputs:
  {
    "IEEERegistry": "MA-L",
    "Id": "FC59C0",
    "MacPrefix": "FC59C0",
    "OrganizationName": "Arista Networks",
    "OrganizationAddress": "5453 Great America Parkway Santa Clara CA US 95054 "
  }
*/
```
</details>

### To install from nuget.org
```powershell
dotnet add package Nox.Reference.MacAddresses
```

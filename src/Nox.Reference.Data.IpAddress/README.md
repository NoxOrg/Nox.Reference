# Nox.Refence.IpAddress project

*Nox.Reference.IpAddress* project contains functionality and classes to store and get information about IP addresses.

## How to use:

There are 2 approaches how to deal with Nox.Reference.IpAddress:

### 1. Static approach: 

Get country code by provided IP address. It determines which country IP belongs to.

```
IpSearchResult result = IpAddressContext.GetCountryByIp(ipAddress);
```

	
### 2. Use dependency injection:
- Initially, World data context should be registered in the dependency container

```
    services.AddIpAddressContext();
```

- Then it simply be acquired using DI

```
    class ConsumerService
	{
		private readonly IIpAddressService _ipAddressService;
		
		public ConsumerService(IIpAddressService ipAddressService)
		{
			_ipAddressService = ipAddressService;
		}
		
		public void Test()
		{
			IpSearchResult result = _ipAddressService.GetCountryByIpAddress(ipAddress);
		}
	}
	
```

- There are 3 overloads of GetCountryByIpAddress method:

```
	/// <summary>
    /// <summary>
    /// Gets country code for ip address by IPAddress object.
    /// </summary>
    /// <param name="ipAddress">.Net type IPAddress</param>
    /// <returns>IpSearchResult</returns>
    /// </summary>
	IpSearchResult GetCountryByIPAddress(IPAddress ipAddress);

    /// <summary>
    /// Gets country code for ip address by IPv4 or ipv6 string.
    /// </summary>
    /// <param name="ipAddress">Ipv4 or Ipv6 address string</param>
    /// <returns></returns>
    IpSearchResult GetCountryByIPAddress(string ipAddress);

    /// <summary>
    /// Gets country code for ip address by IPv4 or IPv6 string.
    /// </summary>
    /// <param name="ipAddressNumber">IPAddress 32 or 128-bit number</param>
    /// <returns></returns>
    IpSearchResult GetCountryByIPAddress(BigInteger ipAddressNumber);
```

*IpSearchResult* is a holder which persists information about search response

```
public class IpSearchResult
{
    public IpSearchResultKind Kind { get; private set; }
    
    /// <summary>
    /// Gets CountryCode in case search was successful.
    /// </summary>
    public string? CountryCode { get; private set; }
}
```

There can be 3 response statuses:

```
public enum IpSearchResultKind
{
    /// <summary>
    /// Ip Address input string has incorrect format.
    /// </summary>
    IncorrectInput,

    /// <summary>
    /// Ip Address has not been found
    /// </summary>
    NotFound = 1,

    /// <summary>
    /// Ip Address has been found
    /// </summary>
    Success = 2
}
```

More examples you can find in  [Nox.Reference.Demo project](https://github.com/NoxOrg/Nox.Reference/blob/main/src/Nox.Demo/Program.cs "Examples")

## How to create migrations:
- In powershel or a similar command tool go to Nox.Reference\src\Nox.Reference.Data.IpAddress
- Run the following command:   

`dotnet ef  migrations add  <MigrationName>  --project ../Nox.Reference.Data.IpAddress/Nox.Reference.Data.IpAddress.csproj`

- Created migration will appear in Nox.Reference.Data.IpAddress\\Migrations folder

To create or update the database:

Run command

`
dotnet ef database update --connection "Data Source=..\\..\\data\\Nox.Reference.Data.IpAddress.db"
`

Nox.Reference.IpAddress.db database file will appear in (RootPath)\data\output\sqlite

## Project structure		
Nox.Reference.IpAddress contains the following entities:

	- IpAddress
		- IpAddressChunk
		
		
***Notes:***
Unlike other projects of Nox.Reference IpAddress doesn't expose any set of data. It only exposes `IpAddressService` and static `IpAddressContext` 
with single method to resolve IP addresses.
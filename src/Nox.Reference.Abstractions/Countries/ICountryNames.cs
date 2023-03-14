using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public interface ICountryNames
{
    public string CommonName { get; }
    public string OfficialName { get; }

    IReadOnlyList<INativeNameInfo>? NativeNames { get; }
}


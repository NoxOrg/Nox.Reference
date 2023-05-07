namespace Nox.Reference.Abstractions;

public interface ICountryNames
{
    public string CommonName { get; }
    public string OfficialName { get; }

    IReadOnlyList<INativeNameInfo>? NativeNames { get; }
}
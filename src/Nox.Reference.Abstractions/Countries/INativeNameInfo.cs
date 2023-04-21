namespace Nox.Reference.Abstractions;

public interface INativeNameInfo
{
    public string Language { get; }
    public string OfficialName { get; }

    public string CommonName { get; }
}
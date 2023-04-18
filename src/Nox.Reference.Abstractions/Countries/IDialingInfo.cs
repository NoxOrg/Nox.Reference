namespace Nox.Reference.Abstractions.Countries;

public interface IDialingInfo
{
    public string Prefix { get; }

    public IReadOnlyList<string> Suffixes { get; }
}
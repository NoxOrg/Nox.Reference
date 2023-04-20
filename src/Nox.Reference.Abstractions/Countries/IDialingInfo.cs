namespace Nox.Reference.Abstractions;

public interface IDialingInfo
{
    public string Prefix { get; }

    public IReadOnlyList<string> Suffixes { get; }
}
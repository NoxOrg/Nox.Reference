namespace Nox.Reference.Abstractions;

public interface IPostalCodeInfo
{
    string Format { get; }
    string Regex { get; }
}
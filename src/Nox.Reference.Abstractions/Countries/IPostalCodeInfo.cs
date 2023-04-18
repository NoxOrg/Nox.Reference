namespace Nox.Reference.Abstractions.Countries;

public interface IPostalCodeInfo
{
    string Format { get; }
    string Regex { get; }
}
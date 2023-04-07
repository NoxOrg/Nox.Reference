namespace Nox.Reference.Abstractions;

public interface INoxReferenceSeed<TType>
{
    void Seed(IEnumerable<TType> data);
}
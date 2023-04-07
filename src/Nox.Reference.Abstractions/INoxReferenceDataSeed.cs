namespace Nox.Reference;

public interface INoxReferenceSeed<TType>
{
    void Seed(IEnumerable<TType> data);
}
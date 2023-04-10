namespace Nox.Reference.Data;

public interface INoxReferenceSeed<TType>
{
    void Seed(IEnumerable<TType> data);
}
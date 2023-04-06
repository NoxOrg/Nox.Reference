namespace Nox.Reference.Common;

public interface INoxReferenceSeed<TType>
{
    void Seed(IEnumerable<TType> data);
}
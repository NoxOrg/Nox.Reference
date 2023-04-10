namespace Nox.Reference.Data.Repositories;

public interface INoxReferenceRepository<TType> where TType : class
{
    TType? Get(int id);
}
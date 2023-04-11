namespace Nox.Reference.Data.Repositories;

public interface INoxReferenceKeyRepository<TType> where TType : class
{
    TType? Get(string key);
}
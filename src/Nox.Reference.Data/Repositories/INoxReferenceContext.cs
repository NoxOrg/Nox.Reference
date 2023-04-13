namespace Nox.Reference.Data.Repositories;

public interface INoxReferenceContext<TType> where TType : class
{
    IQueryable<TType> Set { get; }
}
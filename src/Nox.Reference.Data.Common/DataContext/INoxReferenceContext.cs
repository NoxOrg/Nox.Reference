namespace Nox.Reference.Data;

public interface INoxReferenceContext
{
    IQueryable<TSet> GetSet<TSet>();
}
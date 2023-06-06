namespace Nox.Reference.Data.Common;

public interface IKeyedNoxReferenceEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; }
}
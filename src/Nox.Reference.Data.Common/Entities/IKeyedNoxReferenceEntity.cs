namespace Nox.Reference;

public interface IKeyedNoxReferenceEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; }
}
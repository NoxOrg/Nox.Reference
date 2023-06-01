using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Common;

public abstract class NoxReferenceKeyedEntityConfigurationBase<TEntity, TKey> : NoxReferenceEntityConfigurationBase<TEntity>
    where TEntity : class, INoxReferenceEntity, IKeyedNoxReferenceEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public new void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.Ignore(x => x.Id);
    }
}
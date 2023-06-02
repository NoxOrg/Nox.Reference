using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Common;

public abstract class NoxReferenceEntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : NoxReferenceEntityBase
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.EntityId);
        DoConfigure(builder);
    }

    protected abstract void DoConfigure(EntityTypeBuilder<TEntity> builder);
}
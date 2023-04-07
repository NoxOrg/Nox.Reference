using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Entities;

namespace Nox.Reference.Data.Configurations;

internal abstract class NoxReferenceEntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, INoxReferenceEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);

        DoConfigure(builder);
    }

    protected abstract void DoConfigure(EntityTypeBuilder<TEntity> builder);
}
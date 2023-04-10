using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data;

internal class LocalizationEntityConfigurationBase<TEntity> : NoxReferenceEntityConfigurationBase<TEntity>
    where TEntity : LocalizationEntityBase, INoxReferenceEntity
{
    protected override void DoConfigure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasOne(x => x.Language)
            .WithMany();
    }
}
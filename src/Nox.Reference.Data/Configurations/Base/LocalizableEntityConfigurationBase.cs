using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data;

internal class LocalizableEntityConfigurationBase<TEntity, TLocalization> : NoxReferenceEntityConfigurationBase<TEntity>
    where TEntity : LocalizableEntityBase<TLocalization>, INoxReferenceEntity
    where TLocalization : LocalizationEntityBase
{
    protected override void DoConfigure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasOne(x => x.Localization)
            .WithMany();
    }
}
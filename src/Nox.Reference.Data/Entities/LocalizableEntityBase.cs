namespace Nox.Reference.Data;

internal abstract class LocalizableEntityBase<TLocalization>
    where TLocalization : LocalizationEntityBase
{
    public TLocalization Localization { get; set; }
}

internal abstract class LocalizationEntityBase
{
    public Language Language { get; set; }
}
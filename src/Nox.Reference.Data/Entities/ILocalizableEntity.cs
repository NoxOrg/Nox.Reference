namespace Nox.Reference.Data;

internal interface ILocalizableEntity<TLocalizationType>
{
    public TLocalizationType Localization { get; set; }
}
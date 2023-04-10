namespace Nox.Reference.Data;

internal class CountryLocalization : LocalizationEntityBase, INoxReferenceEntity
{
    public int Id { get; private set; }
    public string OfficialName { get; set; }
    public string CommonName { get; set; }
    public string AlternateSpelling { get; set; }
    public string NameTranslation { get; set; }
}
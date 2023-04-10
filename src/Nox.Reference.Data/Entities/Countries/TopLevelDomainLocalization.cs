namespace Nox.Reference.Data
{
    internal class TopLevelDomainLocalization : LocalizationEntityBase, INoxReferenceEntity
    {
        public int Id { get; private set; }
        public string Name { get; set; }
    }
}
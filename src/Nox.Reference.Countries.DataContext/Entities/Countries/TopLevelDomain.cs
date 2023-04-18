using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext
{
    internal class TopLevelDomain : INoxReferenceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext;

public class CurrencyRareUsage : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; }
}
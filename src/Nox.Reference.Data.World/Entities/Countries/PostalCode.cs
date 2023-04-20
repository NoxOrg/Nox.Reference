using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class PostalCode : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Format { get; set; }
    public string Regex { get; set; }
}
namespace Nox.Reference.Data;

internal class PostalCode : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Format { get; set; }
    public string Regex { get; set; }
}
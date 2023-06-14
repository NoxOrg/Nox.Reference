namespace Nox.Reference;

public class PostalCode : NoxReferenceEntityBase
{
    public string? Format { get; private set; } = string.Empty;
    public string? Regex { get; private set; } = string.Empty;
}
namespace Nox.Reference;

public class TaxNumberValidationRule : NoxReferenceEntityBase
{
    public string TranslationId { get; private set; } = string.Empty;
    public string Regex { get; private set; } = string.Empty;
    public string ValidationFormatDescription { get; private set; } = string.Empty;
    public string InputMask { get; private set; } = string.Empty;
    public int MinimumLength { get; private set; }
    public int MaximumLength { get; private set; }
    public Checksum? Checksum { get; private set; }
}
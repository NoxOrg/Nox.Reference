using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class VatNumberValidationRule : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string TranslationId { get; private set; } = string.Empty;
    public string Regex { get; private set; } = string.Empty;
    public string ValidationFormatDescription { get; private set; } = string.Empty;
    public string InputMask { get; private set; } = string.Empty;
    public int MinimumLength { get; private set; }
    public int MaximumLength { get; private set; }
    public Checksum? Checksum { get; private set; }
}
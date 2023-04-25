using Nox.Reference.Abstractions;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class VatNumberValidationRule : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string TranslationId { get; set; } = string.Empty;
    public string Regex { get; set; } = string.Empty;
    public string ValidationFormatDescription { get; set; } = string.Empty;
    public string InputMask { get; set; } = string.Empty;
    public int MinimumLength { get; set; }
    public int MaximumLength { get; set; }
    public Checksum? Checksum { get; set; }
}

internal class Checksum
{
    public ChecksumAlgorithm? Algorithm { get; set; }
    public string? ChecksumDigit { get; set; }
    public int? Modulus { get; set; }
    public string Weights { get; set; } = string.Empty;
}
﻿namespace Nox.Reference;

public class Culture : NoxReferenceEntityBase,
    IDtoConvertibleEntity<CultureInfo>
{
    public string Name { get; private set; } = string.Empty;
    public string FormalName { get; private set; } = string.Empty;
    public string NativeName { get; private set; } = string.Empty;
    public string? CommonName { get; private set; }
    public string Language { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public string DisplayNameWithDialect { get; private set; } = string.Empty;
    public string CharacterOrientation { get; private set; } = string.Empty;
    public string LineOrientation { get; private set; } = string.Empty;
    public string? LanguageIso_639_2t { get; private set; }
    public virtual Country? Country { get; internal set; }
    public virtual NumberFormat NumberFormat { get; private set; } = null!;
    public virtual DateFormat DateFormat { get; private set; } = null!;

    public CultureInfo ToDto()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        return World.Mapper.Map<CultureInfo>(this);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
using System;
using System.Collections.Generic;

public partial class CountryNameTranslation
{
    public short CountryId { get; set; }

    public short LanguageId { get; set; }

    public string? OfficialName { get; set; }

    public string? CommonName { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;
}

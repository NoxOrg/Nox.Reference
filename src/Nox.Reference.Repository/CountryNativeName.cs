using System;
using System.Collections.Generic;

public partial class CountryNativeName
{
    public short CountryId { get; set; }

    public short LanguageId { get; set; }

    public string OfficialName { get; set; } = null!;

    public string CommonName { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;
}

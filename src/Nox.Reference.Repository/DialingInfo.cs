using System;
using System.Collections.Generic;

namespace Nox.Reference.Repository;

public partial class DialingInfo
{
    public short CountryId { get; set; }

    public string Prefix { get; set; } = null!;

    public short Suffix { get; set; }

    public virtual Country Country { get; set; } = null!;
}

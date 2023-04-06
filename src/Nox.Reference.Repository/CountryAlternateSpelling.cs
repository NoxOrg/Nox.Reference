using System;
using System.Collections.Generic;

namespace Nox.Reference.Repository;

public partial class CountryAlternateSpelling
{
    public short Id { get; set; }

    public short CountryId { get; set; }

    public string Spelling { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;
}

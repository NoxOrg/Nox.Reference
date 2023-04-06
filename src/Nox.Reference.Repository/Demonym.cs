using System;
using System.Collections.Generic;

namespace Nox.Reference.Repository;

public partial class Demonym
{
    public short Id { get; set; }

    public virtual ICollection<DemonymTranslation> DemonymTranslations { get; } = new List<DemonymTranslation>();

    public virtual ICollection<Country> Countries { get; } = new List<Country>();
}

using System;
using System.Collections.Generic;

namespace Nox.Reference.Repository;

public partial class Language
{
    public short Id { get; set; }

    public virtual ICollection<CountryNameTranslation> CountryNameTranslations { get; } = new List<CountryNameTranslation>();

    public virtual ICollection<CountryNativeName> CountryNativeNames { get; } = new List<CountryNativeName>();

    public virtual ICollection<DemonymTranslation> DemonymTranslations { get; } = new List<DemonymTranslation>();

    public virtual ICollection<Country> Countries { get; } = new List<Country>();
}

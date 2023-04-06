using System;
using System.Collections.Generic;

public partial class Continent
{
    public byte Id { get; set; }

    public string PlaceType { get; set; } = null!;

    public virtual ICollection<Country> Countries { get; } = new List<Country>();
}

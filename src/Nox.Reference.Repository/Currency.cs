using System;
using System.Collections.Generic;


public partial class Currency
{
    public short Id { get; set; }

    public virtual ICollection<Country> Countries { get; } = new List<Country>();
}

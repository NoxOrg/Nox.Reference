﻿using System;
using System.Collections.Generic;

public partial class DomainNameExtension
{
    public int Id { get; set; }

    public string Extension { get; set; } = null!;

    public virtual ICollection<Country> Countries { get; } = new List<Country>();
}
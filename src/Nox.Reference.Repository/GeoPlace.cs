using System;
using System.Collections.Generic;

public partial class GeoPlace
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentId { get; set; }

    public short PlaceTypeId { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual GeoPlaceType PlaceType { get; set; } = null!;

    public virtual ICollection<Country> Countries { get; } = new List<Country>();
}

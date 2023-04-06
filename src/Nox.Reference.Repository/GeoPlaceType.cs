using System;
using System.Collections.Generic;

public partial class GeoPlaceType
{
    public short Id { get; set; }

    public string PlaceType { get; set; } = null!;

    public virtual ICollection<GeoPlace> GeoPlaces { get; } = new List<GeoPlace>();
}

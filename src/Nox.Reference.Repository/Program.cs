using System;
using System.Linq;


using var db = new NoxReferencesContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new GeoPlaceType");
db.Add(new GeoPlaceType { Id = 1, PlaceType = "Cityy" });
db.SaveChanges();

// Read
Console.WriteLine("Querying for a GeoPlaceType");
var geoPlaceType = db.GeoPlaceTypes
    .OrderBy(g => g.Id)
    .First();

// Update
Console.WriteLine("Updating the GeoPlaceType");
geoPlaceType.PlaceType = "City";
db.SaveChanges();

// Delete
Console.WriteLine("Delete the City");
db.Remove(geoPlaceType);
db.SaveChanges();
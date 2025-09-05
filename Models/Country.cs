using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}

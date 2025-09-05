using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class City
{
    public int CityId { get; set; }

    public int CountryId { get; set; }

    public string? Name { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Street> Streets { get; set; } = new List<Street>();
}

using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class Street
{
    public int StreetId { get; set; }

    public int CityId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual City City { get; set; } = null!;
}

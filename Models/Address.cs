using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int StreetId { get; set; }

    public string? HouseNumber { get; set; }

    public string? ZipCode { get; set; }

    public virtual ICollection<ParkingLot> ParkingLots { get; set; } = new List<ParkingLot>();

    public virtual Street Street { get; set; } = null!;
}

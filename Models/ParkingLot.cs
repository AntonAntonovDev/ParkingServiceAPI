using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class ParkingLot
{
    public int ParkingLotId { get; set; }

    public string Name { get; set; } = null!;

    public int AddressId { get; set; }

    public string? Description { get; set; }

    public int TotalSpots { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
}

using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class ReservationStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

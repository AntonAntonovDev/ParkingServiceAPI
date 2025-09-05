using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public int UserId { get; set; }

    public int VehicleId { get; set; }

    public int ParkingSpotId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? StatusId { get; set; }

    public virtual ParkingSpot ParkingSpot { get; set; } = null!;

    public virtual ReservationStatus? Status { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}

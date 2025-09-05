using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public int UserId { get; set; }

    public string LicensePlate { get; set; } = null!;

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? Color { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual User User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<UserEmail> UserEmails { get; set; } = new List<UserEmail>();

    public virtual ICollection<UserPhone> UserPhones { get; set; } = new List<UserPhone>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}

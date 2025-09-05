using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class UserEmail
{
    public int UserEmailId { get; set; }

    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public bool? IsMain { get; set; }

    public virtual User User { get; set; } = null!;
}

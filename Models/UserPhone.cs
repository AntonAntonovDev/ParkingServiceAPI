using System;
using System.Collections.Generic;

namespace ParkingServiceApi.Data.Models;

public partial class UserPhone
{
    public int UserPhoneId { get; set; }

    public int UserId { get; set; }

    public string Number { get; set; } = null!;

    public bool? IsMain { get; set; }

    public virtual User User { get; set; } = null!;
}

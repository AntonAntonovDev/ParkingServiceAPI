namespace ParkingServiceApi.Data.Models;

public partial class ParkingSpot
{
    public int ParkingSpotId { get; set; }

    public int ParkingLotId { get; set; }

    public string Number { get; set; } = null!;

    public bool IsOccupied { get; set; }

    public virtual ParkingLot ParkingLot { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

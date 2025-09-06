namespace ParkingServiceApi.DTOs
{
    public class ParkingSpotDto
    {
        public int ParkingSpotId { get; set; }
        public int ParkingLotId { get; set; }
        public string Number { get; set; }
        public bool IsOccupied { get; set; }
    }
}

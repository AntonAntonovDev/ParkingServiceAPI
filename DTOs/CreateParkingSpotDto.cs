namespace ParkingServiceApi.DTOs
{
    public class CreateParkingSpotDto
    {
        public int ParkingLotId { get; set; }
        public string Number { get; set; }
        public bool IsOccupied { get; set; }
    }
}
